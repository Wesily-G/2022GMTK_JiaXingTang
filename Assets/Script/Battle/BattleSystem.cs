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
    //public GameObject playerPrefab;
    //public GameObject enemyPrefab;
    public GameObject skillPanel;
    public GameObject card;

    //public Transform playerBattleStation;
    //public Transform enemyBattleStation;
    
    Unit playerUnit, enemyUnit;
    public TMPro.TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public bool isEven=false;//偶数回合

    string cardName;
    //开始默认为玩家回合
    void Start()
    {
        playerUnit = Generated.playerUnit;
        enemyUnit = Generated.enemyUnit;
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        playerUnit.maxDamage = playerUnit.damage;
    }
    
    IEnumerator SetupBattle()
    {
        //GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);//位置
        //playerUnit = playerGo.GetComponent<Unit>();
        //GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);//位置
        //enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text = "A wild" + enemyUnit.unitName;
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

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
            CardController.Ins.SubCool();
            if (playerUnit.isLuck)
            {
                playerUnit.diceCount = 6;
                playerUnit.isLuck = false;
                return;
            }
            else if (!playerUnit.isFreshman)
                playerUnit.diceCount = (int)Random.Range(1, 1);
            else playerUnit.diceCount = (int)Random.Range(6, 6);
        }
        else if (state == BattleState.ENEMYTURN)
        {
            if (enemyUnit.isControl)
            {
                enemyUnit.diceCount = 1;
                enemyUnit.isControl = false;
                return;
            }
            else if ((int)Random.Range(0, 100) < 80)
                enemyUnit.diceCount = (int)Random.Range(4, 7);
            else if ((int)Random.Range(0, 100) > 90)
                enemyUnit.diceCount = (int)Random.Range(3, 7);
            else enemyUnit.diceCount = (int)Random.Range(1, 7);
        }
    }
    void PlayerTurn()
    {
        ThrowDice();
        dialogueText.text = "Choose an action";
    }

    //返回技能面板
    public void OnBackButton()
    {
        skillPanel.SetActive(true);
        card.SetActive(false);
        //CardController.Ins.Display();
    }
    //逃离战斗（待补全）
    public void OnEscapeButton()
    {
        state = BattleState.ESCAPE;

    }

    IEnumerator PlayerAttack(int force)
    {
        if (!playerUnit.isEnchantment)
            force = force * 1;
        else
        {
            force = (int)(force  + enemyUnit.maxHP * 0.2);
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
            GameManager.Ins.playerStats.characterData.currentLevel++;
            EndBattle();
        }
        else PlayerEndOfTurn();
    }

    public void PlayerEndOfTurn()
    {
        bool isDead = false;
        if (playerUnit.isPoisoned)
        {
            isDead= playerUnit.TakeDamage((int)(0.2 * playerUnit.maxHP));
        }
        if (isDead)
        {
            //战斗结束
            state = BattleState.LOST;
            EndBattle();
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
        ThrowDice();
        bool isDead = false ;
        //添加敌人AI(待修改）
        dialogueText.text = enemyUnit.unitName + "attacks!";
        if (enemyUnit.reducesCount > 0)
        {
            if (!enemyUnit.isRedueces)
            {
                enemyUnit.damage = (int)(0.8 * enemyUnit.damage);
                enemyUnit.isRedueces = true;
            }
            enemyUnit.reducesCount--;
            if (enemyUnit.reducesCount <= 0)
            {
                enemyUnit.damage = (int)(1.25 * enemyUnit.damage);
                enemyUnit.isRedueces = false;
            }
        }
        if (enemyUnit.diceCount == 1)
        {
            if (!enemyUnit.isBoss||!isEven)
            {
                isDead = playerUnit.TakeDamage((int)(enemyUnit.damage * 0.5 * enemyUnit.diceCount));
                if (playerUnit.isBack)
                    enemyUnit.TakeDamage((int)(enemyUnit.damage * 0.5 * enemyUnit.diceCount));
            }
            else playerUnit.isPoisoned = true;
        }
        else
        {
            if (!enemyUnit.isBoss || !isEven)
            {
                isDead = playerUnit.TakeDamage((int)(enemyUnit.damage * (0.2 * (enemyUnit.diceCount + 1))));
                if (playerUnit.isBack)
                    enemyUnit.TakeDamage((int)(enemyUnit.damage * (0.2 * (enemyUnit.diceCount + 1))));
            }
            else
            {
                playerUnit.isRedueces = true;
                playerUnit.damage = (1 - (int)(0.1 * (enemyUnit.diceCount - 1))) * playerUnit.damage;
            }
        }
        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);
        playerUnit.isBack = false;
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
        else StartCoroutine(PlayerAttack(1 * playerUnit.damage)); 
        
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
                playerUnit.damage = playerUnit.maxDamage;
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
        if (Random.Range(0, 100) < 10)
        {
            dialogueText.text = "Miss!";
            PlayerEndOfTurn();
        }
        //enemyUnit.currentHP -= 15 * playerUnit.damage;
        else StartCoroutine(PlayerAttack(10+7 * playerUnit.damage));
    }
    /// <summary>
    /// 减益技能
    /// </summary>
    public void OnReducesDown()
    {
        enemyUnit.reducesCount = 2;
        PlayerEndOfTurn();
    }
}
