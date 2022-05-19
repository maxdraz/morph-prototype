using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawl : PassiveMorph
{
    //[SerializeField] private BrawlPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private bool unlockRage = true;
    [SerializeField] private float rageBonusAttackSpeed = .06f;


    Stats stats;
    Health health;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        health = GetComponent<Health>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);
        
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();
        health = GetComponent<Health>();

        UnsubscribeFromEvents();
        ModifyStats(false);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "Rage")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockRage = true;
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

    private void Update()
    {
        if (unlockRage) 
        {
            if (health.CurrentHealthAsPercentage <= 30) 
            {
                //add bonusAttackSpeed to attackhandler
            }

            if (health.CurrentHealthAsPercentage <= 20)
            {
                //add bonusAttackSpeed to attackhandler
            }

            if (health.CurrentHealthAsPercentage <= 10)
            {
                //add bonusAttackSpeed to attackhandler
            }
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
