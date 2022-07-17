using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : Singleton<CardController>
{
    public CardData[] data;
    CardData nowCard;

    Unit playerUnit;
    Unit enemyUnit;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    protected override void Awake()
    {
        
        base.Awake();
        DontDestroyOnLoad(this);
    }


    //显示卡牌
    public void Display()
    {
        GameObject canv = GameObject.Find("BattleUI/Card"); //找到面板
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].isFind)
            {
                //Debug.Log("jinru");
                GameObject newbodynext = Instantiate(data[i].cardPrefab,new Vector3(-100 * i, 0,0),Quaternion.identity); //生成预制体
                newbodynext.name = data[i].cardName;
                newbodynext.transform.SetParent(canv.transform,false);//再将它设为canvas的子物体
            }
        }
    }

    //找到卡牌并将其状态修改为已被找到
    public void FindIt(string name)
    {
        for(int i = 0; i < data.Length; i++)
        {
            if (!data[i].isFind &&data[i].cardName == name)
            {
                data[i].isFind = true;
                break;
            }
        }
    }

    //使用卡牌
    public void UseCard(string name)
    {
        playerUnit = Generated.playerUnit;
        enemyUnit = Generated.enemyUnit;
        Debug.Log("shiyong");
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].cardName == name)
            {
                nowCard = data[i];
                break;

            }
        }

        if (nowCard!=null && nowCard.coolRemain<1 && nowCard.isFind)
        {
            switch (name)
            {
                case "孤注一掷"://孤注一掷
                    //GameManager.Ins.playerStats.CurrentHealth = 1;
                    playerUnit.currentHP = 1;
                    enemyUnit.currentHP = 1;
                    print("gamble");
                    break;
                case "连击"://连击
                    playerUnit.isBatter = true;
                    print("isBattle");
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
                case "beatBack"://反击
                    playerUnit.isBack = true;
                    break;
            }
            playerHUD.SetHP(playerUnit.currentHP);
            enemyHUD.SetHP(enemyUnit.currentHP);
            nowCard.coolRemain = nowCard.coolTime;

        }
    }

    //减少卡牌的未冷却回合数
    public void SubCool()
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i].coolRemain = Mathf.Max(0, data[i].coolRemain - 1);
        }
    }

    public bool CanBeUse(string name)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].isFind && data[i].cardName == name)
            {
                return data[i].coolRemain < 1;
            }
        }
        return false;
    }
}
