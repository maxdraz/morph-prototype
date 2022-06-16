using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camouflage : PassiveMorph
{
    //[SerializeField] private CamoflagePrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private bool unlockSneaky;
    public float sneakyStealthBonusWhileMoving = .2f;
    
    private Stealth stealth;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        stealth = GetComponent<Stealth>();
    }

    protected override void OnEquip()
    {
        base.OnUnequip();

        ModifyStats(true);

        if (unlockSneaky) 
        {
            Sneaky(sneakyStealthBonusWhileMoving);
        }
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
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
}
