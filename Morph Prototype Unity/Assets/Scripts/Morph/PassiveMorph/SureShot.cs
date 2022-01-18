using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SureShot : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float rangedDamageStatBonus = 5;
    [SerializeField] private bool unlockExpandedReserves = true;
    [SerializeField] private float maxEnergyStatBonus = 5;


    Stats stats;

    private void OnEnable()
    {
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
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeRangedDamageStat(-rangedDamageStatBonus);
        if (unlockExpandedReserves)
        {
            ChangeMaxEnergyStat(-maxEnergyStatBonus);
        }
    }

    // implement
    private void ChangeRangedDamageStat(float amountToAdd)
    {

    }

    private void ChangeMaxEnergyStat(float amountToAdd)
    {

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
