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


    //��ʾ����
    public void Display()
    {
        GameObject canv = GameObject.Find("BattleUI/Card"); //�ҵ����
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].isFind)
            {
                //Debug.Log("jinru");
                GameObject newbodynext = Instantiate(data[i].cardPrefab,new Vector3(-100 * i, 0,0),Quaternion.identity); //����Ԥ����
                newbodynext.name = data[i].cardName;
                newbodynext.transform.SetParent(canv.transform,false);//�ٽ�����Ϊcanvas��������
            }
        }
    }

    //�ҵ����Ʋ�����״̬�޸�Ϊ�ѱ��ҵ�
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

    //ʹ�ÿ���
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
                case "��עһ��"://��עһ��
                    //GameManager.Ins.playerStats.CurrentHealth = 1;
                    playerUnit.currentHP = 1;
                    enemyUnit.currentHP = 1;
                    print("gamble");
                    break;
                case "����"://����
                    playerUnit.isBatter = true;
                    print("isBattle");
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
                case "beatBack"://����
                    playerUnit.isBack = true;
                    break;
            }
            playerHUD.SetHP(playerUnit.currentHP);
            enemyHUD.SetHP(enemyUnit.currentHP);
            nowCard.coolRemain = nowCard.coolTime;

        }
    }

    //���ٿ��Ƶ�δ��ȴ�غ���
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
