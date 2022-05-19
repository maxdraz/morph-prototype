using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camoflage : PassiveMorph
{
    //[SerializeField] private CamoflagePrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private bool unlockSneaky;
    public float sneakyStealthBonusWhileMoving = .2f;
    Stats stats;
    Stealth stealth;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        stealth = GetComponent<Stealth>();
        StartCoroutine(AssignDamageHandlerCoroutine());
        ModifyStats(true);

        if (unlockSneaky) 
        {
            Sneaky(sneakyStealthBonusWhileMoving);
        }
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();
        stealth = GetComponent<Stealth>();

        UnsubscribeFromEvents();
        ModifyStats(false);

        if (unlockSneaky)
        {
            Sneaky(-sneakyStealthBonusWhileMoving);
        }
    }

    public void UnlockSecondary(string name)
    {
        if (name == "Sneaky")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockSneaky = true;
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

    private void Sneaky(float amountToAdd)
    {
        stealth.stealthModifierWhileMoving += amountToAdd;
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
