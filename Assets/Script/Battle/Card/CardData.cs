using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardEffect
{
    TWICE,      //连击
    DAMAGE,     //孤注一掷
    CURE,       //圣洁
    FORCE,      //控制
    LUCKY,      //幸运    
    FUMO        //附魔
}
[Serializable]
public class CardData 
{
    public bool isFind;
    [Header("基本数据")]
    public int cardID;
    public string cardName;
    public string cardDes;
    
    public GameObject cardPrefab;

    [Header("卡牌效果")]
    public int effectNum;
    public CardEffect effect;

    [Header("卡牌冷却")]
    public int coolTime;
    public int coolRemain;
}
