using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SureShot : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int rangedDamageStatBonus = 5;
    [SerializeField] private bool unlockExpandedReserves = true;
    [SerializeField] private float maxEnergyStatBonus = 5;

    Energy energy;
    Stats stats;

    private void OnEnable()
    {
        energy = GetComponent<Energy>();
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeRangedDamageStat(rangedDamageStatBonus);

        if (unlockExpandedReserves) 
        {
            ChangeMaxEnergyStat(maxEnergyStatBonus);
        }
    }

    private void OnDisable()
    {
        energy = GetComponent<Energy>();
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeRangedDamageStat(-rangedDamageStatBonus);
        if (unlockExpandedReserves)
        {
            ChangeMaxEnergyStat(-maxEnergyStatBonus);
        }
    }

    // implement
    private void ChangeRangedDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("rangedDamage", amountToAdd);
    }

    private void ChangeMaxEnergyStat(float amountToAdd)
    {
        energy.bonusMaxEnergy += amountToAdd;
        energy.SetMaxEnergy();
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
