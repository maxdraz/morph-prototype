using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : PassiveMorph
{
    //[SerializeField] private StalkerPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private bool unlockSniper;
    [SerializeField] private int sniperBonusPercentDamage = 5;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ModifyStats(false);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "Sniper")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockSniper = true;
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


    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        //if (damageTakenSummary.isRangedAttack && damageTakenSummary.isStealthAttack)
        //{
        //    damageTakenSummary.DamageTaker.ApplyDamage(new PhysicalDamageData(damageTakenSummary.PhysicalDamage * sniperBonusPercentDamage), damageHandler);
        //}
    }

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;

        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            

        }

        damageHandler = null;
    }
}
