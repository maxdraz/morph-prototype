using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleReady : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int meleeDamageStatBonus = 5;
    [SerializeField] private int rangedDamageStatBonus = 5;
    [SerializeField] private bool unlockBattleMaster = true;
    [SerializeField] private float citChance = 5;


    Stats stats;

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(meleeDamageStatBonus);
        ChangeRangedDamageStat(rangedDamageStatBonus);
        stats = GetComponent<Stats>();

        if (unlockBattleMaster) 
        {
            //gain critchance
        }
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeMeleeDamageStat(-meleeDamageStatBonus);
        ChangeRangedDamageStat(-rangedDamageStatBonus);

        if (unlockBattleMaster)
        {
            //lose critchance
        }
    }

    // implement
    private void ChangeMeleeDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("meleeDamage", amountToAdd);
    }

    private void ChangeRangedDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("meleeDamage", amountToAdd);
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
