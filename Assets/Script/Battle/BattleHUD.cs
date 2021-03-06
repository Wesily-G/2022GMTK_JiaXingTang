using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ս????Ϣ??ʾ
/// </summary>
public class BattleHUD : MonoBehaviour
{
    public TMPro.TMP_Text nameText;
    public TMPro.TMP_Text levelText;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
