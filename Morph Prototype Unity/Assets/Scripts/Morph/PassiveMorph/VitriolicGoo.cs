using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VitriolicGoo : PassiveMorph
{
    static int chemicalDamagePrerequisit = 50;


    private DamageHandler damageHandler;
    [SerializeField] private float staminaDrainFraction;
    [SerializeField] private bool unlockMolecularAcid;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);
        
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ModifyStats(false);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "MolecularAcid")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockMolecularAcid = true;
        }
    }

    // If the bool AddToStat is set to positive it will add to the stats, if negative it will remove from the stats
    void ModifyStats(bool AddToStat)
    {
        if (stats != null)
        {
            if (statsToModify.Length > 0)
            {
                for (int i = 0; i <= statsToModify.Length - 1; i++)
                {
                    if (AddToStat)
                    {
                        Debug.Log(GetType().Name + " is adding" + statsToModify[i].value + " to " + statsToModify[i].stat);
                        stats.FlatStatChange(statsToModify[i].stat.ToString(), statsToModify[i].value);
                    }
                    else
                    {
                        Debug.Log(GetType().Name + " is removing" + statsToModify[i].value + " from " + statsToModify[i].stat);
                        stats.FlatStatChange(statsToModify[i].stat.ToString(), -statsToModify[i].value);
                    }
                }
            }
        }
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
