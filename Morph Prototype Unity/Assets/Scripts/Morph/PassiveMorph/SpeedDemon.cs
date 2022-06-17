using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDemon : PassiveMorph
{
    static int agilityPrerequisit = 40;

    [SerializeField] private bool unlockCruelCapacity;

    int agilityPerStack;
    int bonusAgility;
    int currentSpeedDemonStacks;
    [SerializeField] private float startingSpeedDemonStackDuration;
    float speedDemonStackDuration;

    int cruelCapacityAttackSpeedThreshold;
    [SerializeField] private float cruelCapacityCooldownReduction = 0.2f;
    private bool cruelCapacityActivated;

    protected override void OnEquip()
    {
        base.OnEquip();

        ModifyStats(true);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ModifyStats(false);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "CruelCapacity")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockCruelCapacity = true;
        }
    }

    // If the bool AddToStat is set to positive it will add to the stats, if negative it will remove from the stats
    void ModifyStats(bool AddToStat)
    {
        if (!stats || statsToModify.Length <= 0) return;
        
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
        
        
        if(!unlockCruelCapacity) return;
        TryActivateCruelCapacity(Stats.attackSpeed, stats.GetStat(Stats.attackSpeed));
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {

        if (damageTakenSummary.IsCriticalHit && damageTakenSummary.isMeleeAttack)
        {
            AddSpeedDemonStack();
        }
    }

    void AddSpeedDemonStack() 
    {
        currentSpeedDemonStacks++;
        bonusAgility = currentSpeedDemonStacks * agilityPerStack;
        StopCoroutine("DecaySpeedDemonStacks");
        StartCoroutine("DecaySpeedDemonStacks");
    }

    void RemoveSpeedDemonStack()
    {
        currentSpeedDemonStacks--;
        bonusAgility = currentSpeedDemonStacks * agilityPerStack;

        if (currentSpeedDemonStacks > 0) 
        {
            StartCoroutine("DecaySpeedDemonStacks");
        }
    }

    IEnumerator DecaySpeedDemonStacks() 
    {

        if (currentSpeedDemonStacks > 1)
        {
            speedDemonStackDuration = startingSpeedDemonStackDuration * Mathf.Pow(.9f, currentSpeedDemonStacks);
        }
        else 
        {
            speedDemonStackDuration = startingSpeedDemonStackDuration;
        }

        yield return new WaitForSeconds(speedDemonStackDuration);

        RemoveSpeedDemonStack();

        yield return null;
    }

    private void ApplyCruelCapacity(bool applyOrReverse)
    {
        if(stats.TryGetStat(Stats.cooldownReductionMultiplier, out var val))
        {
            var sign = applyOrReverse ? -1 : 1;
            stats.SetStat(Stats.cooldownReductionMultiplier, val + (sign * cruelCapacityCooldownReduction));
            cruelCapacityActivated = applyOrReverse;
        }
    }
    
    private void TryActivateCruelCapacity(string statKey, float value) 
    {
        //Need to reduce all cooldowns and reduce all energy costs, when totalAttackSpeed  is above a certain threshold

        if (!unlockCruelCapacity || statKey != Stats.attackSpeed) return;

        if (value < cruelCapacityAttackSpeedThreshold && cruelCapacityActivated)
        {
            //remove buff
            ApplyCruelCapacity(false);
            return;
        }

        if (value >= cruelCapacityAttackSpeedThreshold && !cruelCapacityActivated)
        {
            //add buff
            ApplyCruelCapacity(true);
        }
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();

        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
        }

        if (stats) stats.StatHasBeenModified += TryActivateCruelCapacity;
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
        }
        
        if (stats) stats.StatHasBeenModified -= TryActivateCruelCapacity;
    }
}
