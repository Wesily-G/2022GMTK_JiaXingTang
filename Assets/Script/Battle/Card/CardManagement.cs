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
        //��ÿ���Ч��
        switch (name)
        {
            case "gamble"://��עһ��
                GameManager.Ins.playerStats.CurrentHealth = 1;
                enemyUnit.currentHP = 1;
                CardController.Ins.FindIt(name);
                break;
            case "batter"://����
                playerUnit.isBatter = true;
                break;
            case "holy"://ʥ��
                playerUnit.Heal(playerUnit.maxHP / 2);
                break;
            case "enchantment"://��ħ
                playerUnit.isEnchantment = true;
                break;
            case "control"://����
                enemyUnit.isControl = true;
                break;
            case "Luck"://����
                playerUnit.isLuck = true;
                break;

        }
    }
}
