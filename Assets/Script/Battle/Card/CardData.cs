using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardEffect
{
    TWICE,      //����
    DAMAGE,     //��עһ��
    CURE,       //ʥ��
    FORCE,      //����
    LUCKY,      //����    
    FUMO        //��ħ
}
[Serializable]
public class CardData 
{
    public bool isFind;
    [Header("��������")]
    public int cardID;
    public string cardName;
    public string cardDes;
    
    public GameObject cardPrefab;

    [Header("����Ч��")]
    public int effectNum;
    public CardEffect effect;

    [Header("������ȴ")]
    public int coolTime;
    public int coolRemain;
}
