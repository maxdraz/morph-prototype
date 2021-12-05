using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VitriolicGoo : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float chemicalDamageStatBonus = 5;
    [SerializeField] private bool unlockMolecularAcid = true;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeChemicalDamageStat(chemicalDamageStatBonus);
        
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeChemicalDamageStat(-chemicalDamageStatBonus);
    }

    // implement
    private void ChangeChemicalDamageStat(float amountToAdd)
    {
   
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.AcidDamage > 0)
        {
            damageTakenSummary.DamageTaker.ApplyDamage(new StaminaDrainData(damageTakenSummary.AcidDamage), damageHandler);
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
