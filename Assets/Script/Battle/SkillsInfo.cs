using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillsInfo : MonoBehaviour
{
    public TMPro.TMP_Text skillInfo;
    private void Start()
    {

    }
    public void DirectDamage()
    {
        skillInfo.text = "DirectDamage";
    }
    public void Purify()
    {
        skillInfo.text = "Purify";
    }
    public void Damage()
    {
        skillInfo.text = "Damage";
    }
    public void Reduces()
    {
        skillInfo.text = "Reduces";
    }
}
