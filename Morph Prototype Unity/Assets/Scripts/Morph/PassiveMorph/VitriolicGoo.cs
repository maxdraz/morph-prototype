using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VitriolicGoo : PassiveMorph
{
    static int chemicalDamagePrerequisit = 50;


    private DamageHandler damageHandler;
    [SerializeField] private float staminaDrainFraction;
    [SerializeField] private int chemicalDamageStatBonus = 5;
    [SerializeField] private bool unlockMolecularAcid = true;

    Stats stats;

    //public Prerequisite[] StatPrerequisits;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeChemicalDamageStat(chemicalDamageStatBonus);
        
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeChemicalDamageStat(-chemicalDamageStatBonus);
    }

    // implement
    private void ChangeChemicalDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("chemicalDamage", amountToAdd);
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.AcidDamage > 0)
        {
            damageTakenSummary.DamageTaker.ApplyDamage(new StaminaDrainData(damageTakenSummary.AcidDamage * staminaDrainFraction), damageHandler);
        }
    }

    private void OnAcidDebuffDealt(ref IDamageType damageType, DamageHandler damageTaker)
    {
        if (damageType is IAcidDamage acidDamage)
        {
            BuffAcidDamage(ref acidDamage);
            ShortenAcidDOTDuration(ref acidDamage);
        }
    }
    
    // implement
    private void BuffAcidDamage(ref IAcidDamage acidDamage)
    {
        acidDamage.AcidDamage *= 1.5f;
    }

    private void ShortenAcidDOTDuration(ref IAcidDamage acidDamage)
    {
        acidDamage.AcidDOTDuration = 3;
    }

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void GetReferencesAndSubscribeToEvenets()
    {
        if(damageHandler) return;
        
        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
            if (unlockMolecularAcid)
            {
                damageHandler.DebuffAboutToBeDealtPreModifier += OnAcidDebuffDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            if (unlockMolecularAcid)
            {
                damageHandler.DebuffAboutToBeDealtPreModifier -= OnAcidDebuffDealt;
            }
           
        }

        damageHandler = null;
    }
}
