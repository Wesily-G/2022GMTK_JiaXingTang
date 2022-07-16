using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    //…À∫¶÷µ
    public int damage;

    public int maxHP;
    public int currentHP;

    //BUFF
    public bool isBatter;
    public bool isEnchantment;
    public bool isControl;
    public bool isLuck;
    public bool isFreshman;
    public bool isPoisoned;
    public int reducesCount;


    public int diceCount;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
            return true;
        else return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

}
