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
        //��ÿ���Ч��
        switch (name)
        {
            case "gamble"://��עһ��
                playerUnit.currentHP = 1;
                enemyUnit.currentHP = 1;
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
