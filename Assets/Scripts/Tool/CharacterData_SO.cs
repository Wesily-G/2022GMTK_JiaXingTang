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

    //TODO:����������ֵ

    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public float levelBuff;

    [Header("Attack")]
    public int Damage;
    public float criticalChance;//������
    public float criticalMultiplier;//�������Ӱٷֱ�

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
        //����������
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
        baseExp += (int)(baseExp * LevelMultiply);

        maxHealth = (int)(maxHealth + levelBuff);
        Damage += 2;
       currentHealth = maxHealth;

    }
}
