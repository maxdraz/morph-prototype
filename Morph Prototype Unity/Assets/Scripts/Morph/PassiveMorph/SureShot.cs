using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SureShot : PassiveMorph
{
    //[SerializeField] private SureShotPrerequisiteData prerequisiteData;


    //private DamageHandler damageHandler;
    [SerializeField] private bool unlockExpandedReserves = true;
    [SerializeField] private float maxEnergyStatBonus = .2f;

    [SerializeField] private Energy energy;
    
    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        energy = GetComponent<Energy>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ModifyStats(true);
        
        if (unlockExpandedReserves) //TODO needs to be implemented
        {
            ChangeMaxEnergyStat(maxEnergyStatBonus);
        }
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ModifyStats(false);
        
        if (unlockExpandedReserves)
        {
            ChangeMaxEnergyStat(-maxEnergyStatBonus);
        }
    }

    public void UnlockSecondary (string name) 
    {
        if (name == "ExpandedReserves") 
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockExpandedReserves = true;
        }      
    }

    // If the bool AddToStat is set to positive it will add to the stats, if negative it will remove from the stats
    void ModifyStats(bool AddToStat)
    {
        if (stats != null)
        {
            if (statsToModify.Length > 0)
            {
                for (int i = 0; i <= statsToModify.Length -1; i++)
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

    private void ChangeMaxEnergyStat(float amountToAdd)
    {
        energy.bonusMaxEnergy += amountToAdd;
        energy.SetMaxEnergy();
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();

        if (morphLoadout)
        {
            morphLoadout.MorphLoadoutChanged += CheckExpandedReservesPrerequisites;
        }
    }
    
    protected override void UnsubscribeEvents()
    {
        base.SubscribeEvents();

        if (morphLoadout)
        {
            morphLoadout.MorphLoadoutChanged -= CheckExpandedReservesPrerequisites;
        }
    }

    private void CheckExpandedReservesPrerequisites(Morph morphAdded)
    {
        if(unlockExpandedReserves) return;
        
        CheckSecondaryPrerequisites(morphLoadout, stats);
        
        // if prerequistie check returns true
            // modify stats
            // unlockExpandedReserves = true
    }
}
