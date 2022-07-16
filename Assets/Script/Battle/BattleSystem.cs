using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//战斗系统 骰子概率待修改 暂未实现调戏旁白功能
/// <summary>
/// 当前回合状态
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
    //开始默认为玩家回合
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
        //播放投骰子动画(待补全）
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
        //判断点击的是哪张卡（待补全）
        //cardName=
        CardManagement.UseCard(playerUnit, enemyUnit, cardName);
        playerHUD.SetHP(playerUnit.currentHP);
        enemyHUD.SetHP(enemyUnit.currentHP);
        PlayerEndOfTurn();
    }
    //逃离战斗（待补全）
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
        //攻击敌人
        bool isDead=enemyUnit.TakeDamage(force);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";
        yield return new WaitForSeconds(2f);
        //检查敌人死亡状态，根据结果改变状态
        if (isDead)
        {
            //战斗结束
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
            //下一回合
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            //下一回合
            state = BattleState.PLAYERTURN;
            playerUnit.isBatter = false;
            PlayerTurn();
        }
    }

    IEnumerator EnemyTurn()
    {
        bool isDead;
        //添加敌人AI(待修改）
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

    //结束战斗（待补全）
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

    //技能
    /// <summary>
    /// 直接攻击技能
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
    /// 净化技能
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
    /// 直接攻击技能
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
    /// 减益技能
    /// </summary>
    public void OnReducesDown()
    {
        if (Random.Range(0, 100) < 75)
            enemyUnit.reducesCount = 2;
        else dialogueText.text = "Miss!";
        PlayerEndOfTurn();
    }
}
