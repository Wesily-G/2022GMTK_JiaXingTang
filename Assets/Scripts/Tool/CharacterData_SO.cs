using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]

public class CharacterData_SO : ScriptableObject
{
    public string name;
    [Header("Stats Info")]
    public int maxHealth;
    public int currentHealth;
    public int baseDefence;
    public int currentDefence;

    [Header("Being killed")]
    public int killPoint;

    //TODO:人物其他数值

    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public float levelBuff;

    [Header("Attack")]
    public int Damage;
    public float criticalChance;//暴击率
    public float criticalMultiplier;//暴击增加百分比

    public float LevelMultiply
    {
        get { return 1 + (currentLevel - 1) * levelBuff; }
    }
    public void UpdateExp(int point)
    {
        currentExp += point;
        if (currentExp >= baseExp)
            levelUp();
    }
     
    private void levelUp()
    {
        //提升的属性
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
        baseExp += (int)(baseExp * LevelMultiply);

        maxHealth = (int)(maxHealth + levelBuff);
        Damage += 2;
       currentHealth = maxHealth;

    }
}
