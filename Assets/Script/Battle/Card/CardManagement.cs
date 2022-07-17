using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManagement : MonoBehaviour
{
    public static CardManagement instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public static void UseCard(Unit playerUnit, Unit enemyUnit, string name)
    {
        //if(CardController.Ins.CanBeUse(name))
        //获得卡牌效果
        switch (name)
        {
            case "gamble"://孤注一掷
                GameManager.Ins.playerStats.CurrentHealth = 1;
                enemyUnit.currentHP = 1;
                CardController.Ins.FindIt(name);
                break;
            case "batter"://连击
                playerUnit.isBatter = true;
                break;
            case "holy"://圣洁
                playerUnit.Heal(playerUnit.maxHP / 2);
                break;
            case "enchantment"://附魔
                playerUnit.isEnchantment = true;
                break;
            case "control"://控制
                enemyUnit.isControl = true;
                break;
            case "Luck"://幸运
                playerUnit.isLuck = true;
                break;

        }
    }
}
