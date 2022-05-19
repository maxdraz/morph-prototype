using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frills : PassiveMorph
{
    //[SerializeField] private FrillsPrerequisiteData prerequisiteData;

    private DamageHandler damageHandler;
    [SerializeField] private bool unlockFearless;
    [SerializeField] private float fearlessIntimidationDefenseBonus = .3f;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);

        if (unlockFearless)
        {
            Fearless(fearlessIntimidationDefenseBonus);
        }
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ModifyStats(false);

        if (unlockFearless) 
        {
            Fearless(-fearlessIntimidationDefenseBonus);
        }
    }

    public void UnlockSecondary(string name)
    {
        if (name == "Fearless")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockFearless = true;
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

    private void Fearless(float amount) 
    {
        GetComponent<Intimidation>().defenseModifier += amount;
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
