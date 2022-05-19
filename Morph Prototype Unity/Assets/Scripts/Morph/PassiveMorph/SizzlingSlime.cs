using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizzlingSlime : PassiveMorph
{
    static int chemicalDamagePrerequisit = 35;


    private DamageHandler damageHandler;

    [SerializeField] private float perceptionDamageFraction;
    [SerializeField] private int chemicalDamageStatBonus = 5;
    [SerializeField] private bool unlockBlindingVapour;

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
        if (name == "BlindingVapour")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockBlindingVapour = true;
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
            damageTakenSummary.DamageTaker.ApplyDamage(new PerceptionDamageData(damageTakenSummary.AcidDamage * perceptionDamageFraction), damageHandler);
        }
    }

    private void OnAcidDamageAboutToBeDealt(ref IDamageType damageType)
    {
        if (damageType is IAcidTickDamage acidTickDamage)
        {
            acidTickDamage.AcidDamage *= 2;
        }
    }
    
    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {
            damageHandler.DamageAboutToBeDealt += OnAcidDamageAboutToBeDealt;
            if (unlockBlindingVapour)
            {
                damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageAboutToBeDealt -= OnAcidDamageAboutToBeDealt;
            if (unlockBlindingVapour)
            {
                damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            }

        }

        damageHandler = null;
    }
}
