using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action<int, int> UpdateHealthBarOnAttack;
    public CharacterData_SO characterData;
    public CharacterData_SO templateData;

    [HideInInspector]
    public bool isCritical;

    private void Awake()
    {
        if (templateData != null)
            characterData = Instantiate(templateData);
    }
    #region �����������
    public int MaxHealth
    {
        get
        {
            if (characterData != null)
                return characterData.maxHealth;
            else
                return 0;
        }
        set
        {
            characterData.maxHealth = value;
        }
    }
    public int CurrentHealth
    {
        get
        {
            if (characterData != null)
                return characterData.currentHealth;
            else
                return 0;
        }
        set
        {
            characterData.currentHealth = value;
        }
    }
    public int BaseDefence
    {
        get
        {
            if (characterData != null)
                return characterData.baseDefence;
            else
                return 0;
        }
        set
        {
            characterData.baseDefence = value;
        }
    }
    public int CurrentDefence
    {
        get
        {
            if (characterData != null)
                return characterData.currentDefence;
            else
                return 0;
        }
        set
        {
            characterData.currentDefence = value;
        }

    }
    #endregion

    #region  ���﹥������
    public void TakeDamage(CharacterStats attacker, CharacterStats defener)
    {
        int damage = Mathf.Max(attacker.CurrentDamage() - defener.CurrentDefence, 1);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        if (attacker.isCritical)
        {
            defener.GetComponent<Animator>().SetTrigger("Hit");
        }
        //TODO:Update UI
        UpdateHealthBarOnAttack?.Invoke(defener.CurrentHealth, defener.MaxHealth);
        //TODO:�������
        if (CurrentHealth <= 0)
        {
            attacker.characterData.UpdateExp(characterData.killPoint);
            
        }
    }
    public void TakeDamage(int damage, CharacterStats defener)
    {
        int finalDamage = Mathf.Max(damage - defener.CurrentDefence, 1);
        CurrentHealth = Mathf.Max(CurrentHealth - finalDamage, 0);
        //if (attacker.isCritical)
        //{
        //    defener.GetComponent<Animator>().SetTrigger("Hit");
        //}
        //TODO:Update UI
        UpdateHealthBarOnAttack?.Invoke(defener.CurrentHealth, defener.MaxHealth);
    }

    //��ɱ����˺�
    private int CurrentDamage()
    {
        float coreDamage = characterData.Damage;
        if (isCritical)
        {
            coreDamage = coreDamage * characterData.criticalMultiplier;
            Debug.Log("����" + coreDamage);
        }
        return (int)coreDamage;
    }

    #endregion

}
