using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitch : MonoBehaviour
{
    public GameObject Main, Skill, Card;
    public void OnMain()
    {
        Skill.SetActive(false);
        Card.SetActive(false);
        Main.SetActive(true);
    }
    public void OnSkill()
    {
        Main.SetActive(false);
        Skill.SetActive(true);
    }
    public void OnCard()
    {
        
        Main.SetActive(false);
        Card.SetActive(true);
        CardController.Ins.Display();
    }
}
