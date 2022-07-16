using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ս��ϵͳ ���Ӹ��ʴ��޸� ��δʵ�ֵ�Ϸ�԰׹���
/// <summary>
/// ��ǰ�غ�״̬
/// </summary>
public enum BattleState
{
    NULL,
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST,
    ESCAPE
}
public class BattleSystem : MonoBehaviour
{

    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    
    Unit playerUnit, enemyUnit;
    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public CardHUD cardHUD;

    string cardName;
    //��ʼĬ��Ϊ��һغ�
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    
    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();
        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text = "A wild" + enemyUnit.unitName;
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        ThrowDice();
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    void ThrowDice()
    {
        //����Ͷ���Ӷ���(����ȫ��
        if(state == BattleState.WON || state == BattleState.LOST)
        {
            return;
        }
        if (state == BattleState.PLAYERTURN)
        {
            if (playerUnit.isLuck)
            {
                playerUnit.diceCount = 6;
                playerUnit.isLuck = false;
                return;
            }
            else playerUnit.diceCount = (int)Random.Range(1, 7);
        }
        else if (state == BattleState.ENEMYTURN)
        {
            if (enemyUnit.isControl)
            {
                enemyUnit.diceCount = 1;
                enemyUnit.isControl = false;
                return;
            }
            else enemyUnit.diceCount= (int)Random.Range(1, 7);
        }
    }
    void PlayerTurn()
    {
        dialogueText.text = "Choose an action";
    }

    public void OnCardButton()
    {
        //�жϵ���������ſ�������ȫ��
        //cardName=
        CardManagement.UseCard(playerUnit, enemyUnit, cardName);
        playerHUD.SetHP(playerUnit.currentHP);
        enemyHUD.SetHP(enemyUnit.currentHP);
        PlayerEndOfTurn();
    }
    //����ս��������ȫ��
    public void OnEscapeButton()
    {
        state = BattleState.ESCAPE;

    }

    //public void OnHealButton()
    //{
    //    if (state != BattleState.PLAYERTURN)
    //        return;
    //    StartCoroutine(PlayerHeal());
    //}

    IEnumerator PlayerAttack(int force)
    {
        if (!playerUnit.isEnchantment)
            force = (int)(force * (60 + 20 * (playerUnit.diceCount - 1)));
        else
        {
            force = (int)(force * (60 + 20 * (playerUnit.diceCount - 1)) + (int)enemyUnit.maxHP * 0.2);
            playerUnit.isEnchantment = false;
        }
        //��������
        bool isDead=enemyUnit.TakeDamage(force);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";
        yield return new WaitForSeconds(2f);
        //����������״̬�����ݽ���ı�״̬
        if (isDead)
        {
            //ս������
            state = BattleState.WON;
            EndBattle();
        }
        else PlayerEndOfTurn();
    }

    public void PlayerEndOfTurn()
    {
        if (playerUnit.isPoisoned)
        {

        }
        if (!playerUnit.isBatter)
        {
            //��һ�غ�
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            //��һ�غ�
            state = BattleState.PLAYERTURN;
            playerUnit.isBatter = false;
            PlayerTurn();
        }
    }

    IEnumerator EnemyTurn()
    {
        bool isDead;
        //��ӵ���AI(���޸ģ�
        dialogueText.text = enemyUnit.unitName + "attacks!";
        yield return new WaitForSeconds(1f);
        if (enemyUnit.reducesCount <= 0)
        {
            isDead = playerUnit.TakeDamage(enemyUnit.damage * (60 + 20 * (enemyUnit.diceCount - 1)));
        }
        else
        {
            isDead = playerUnit.TakeDamage((int)0.5*enemyUnit.damage * (60 + 20 * (enemyUnit.diceCount - 1)));
            enemyUnit.reducesCount--;
        }
            playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    //����ս��������ȫ��
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Won!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Defeated!";
        }
    }

    //����
    /// <summary>
    /// ֱ�ӹ�������
    /// </summary>
    public void OnDirectDamageButton()
    {
        if (playerUnit.isFreshman)
            StartCoroutine(PlayerAttack(999 * playerUnit.damage));
        //enemyUnit.currentHP -= 999 * playerUnit.damage;
        else StartCoroutine(PlayerAttack(1 * playerUnit.damage)); 
        //enemyUnit.currentHP -= 1;
        
    }
    /// <summary>
    /// ��������
    /// </summary>
    public void OnPurifyButton()
    {
        if (playerUnit.isFreshman)
        {
            if (Random.Range(0, 100) < 99)
            {
                playerUnit.isPoisoned = false;
            }
        }
        else dialogueText.text = "Failed!";
        PlayerEndOfTurn();
    }
    /// <summary>
    /// ֱ�ӹ�������
    /// </summary>
    public void OnDamageButton()
    {
        if (Random.Range(0, 100) < 50)
        {
            dialogueText.text = "Miss!";
            PlayerEndOfTurn();
        }
        //enemyUnit.currentHP -= 15 * playerUnit.damage;
        else StartCoroutine(PlayerAttack(15 * playerUnit.damage));
    }
    /// <summary>
    /// ���漼��
    /// </summary>
    public void OnReducesDown()
    {
        if (Random.Range(0, 100) < 75)
            enemyUnit.reducesCount = 2;
        else dialogueText.text = "Miss!";
        PlayerEndOfTurn();
    }
}
