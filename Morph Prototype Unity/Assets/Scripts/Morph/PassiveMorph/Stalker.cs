using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int rangedDamageStatBonus = 5;
    [SerializeField] private int stealthStatBonus = 5;
    [SerializeField] private bool unlockSniper = true;
    [SerializeField] private int sniperBonusPercentDamage = 5;

    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeRangedDamageStat(rangedDamageStatBonus);
        ChangeStealthStat(stealthStatBonus);
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeRangedDamageStat(-rangedDamageStatBonus);
        ChangeStealthStat(-stealthStatBonus);
    }

    // implement
    private void ChangeRangedDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("rangedDamage", amountToAdd);
    }

    private void ChangeStealthStat(int amountToAdd)
    {
        stats.FlatStatChange("stealth", amountToAdd);
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
