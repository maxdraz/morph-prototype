using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemical : PassiveMorph
{
    //[SerializeField] private ChemicalPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private int chemicalDamageStatBonus = 5;
    [SerializeField] private bool unlockConcentratedChemicals = true;
    [SerializeField] private float bonusToughnessReduction = .1f;
    [SerializeField] private float bonusHealingReduction = .2f;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeChemicalDamageStat(chemicalDamageStatBonus);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeChemicalDamageStat(-chemicalDamageStatBonus);
    }

    // implement
    private void ChangeChemicalDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("chemicalDamage", amountToAdd);
    }



    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void IncreaseAcidDebuff(ref IAcidDamage acidDamage) 
    {
        //acidDamage.toughnessReduction += bonusToughnessReduction;
    }

    private void IncreasePoisonDebuff(ref IPoisonDamage poisonDamage)
    {
        //acidDamage.healingReduction += bonusHealingReduction;
    }

    private void OnAcidDebuffDealt(ref IDamageType damageType, DamageHandler damageTaker)
    {
        if (unlockConcentratedChemicals) 
        {
            if (damageType is IAcidDamage acidDamage)
            {
                IncreaseAcidDebuff(ref acidDamage);
            }    
        }
    }

    private void OnPoisonDebuffDealt(ref IDamageType damageType, DamageHandler damageTaker)
    {
        if (unlockConcentratedChemicals)
        {
            if (damageType is IPoisonDamage poisonDamage)
            {
                IncreasePoisonDebuff(ref poisonDamage);
            }
        }
    }
        

private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {
            if (unlockConcentratedChemicals) 
            {
                damageHandler.DebuffAboutToBeDealtPreModifier += OnAcidDebuffDealt;
                damageHandler.DebuffAboutToBeDealtPreModifier += OnPoisonDebuffDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            if (unlockConcentratedChemicals)
            {
                damageHandler.DebuffAboutToBeDealtPreModifier -= OnAcidDebuffDealt;
                damageHandler.DebuffAboutToBeDealtPreModifier -= OnPoisonDebuffDealt;
            }
        }

        damageHandler = null;
    }
}
