using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleReady : PassiveMorph
{
    //[SerializeField] private BattleReadyPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private int meleeDamageStatBonus = 5;
    [SerializeField] private int rangedDamageStatBonus = 5;
    [SerializeField] private bool unlockBattleMaster = true;
    [SerializeField] private float critChance = 5;


    Stats stats;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(meleeDamageStatBonus);
        ChangeRangedDamageStat(rangedDamageStatBonus);

        if (unlockBattleMaster) 
        {
            stats.globalCritChance += critChance;
        }
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeMeleeDamageStat(-meleeDamageStatBonus);
        ChangeRangedDamageStat(-rangedDamageStatBonus);

        if (unlockBattleMaster)
        {
            stats.globalCritChance -= critChance;
        }
    }

    // implement
    private void ChangeMeleeDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("meleeDamage", amountToAdd);
    }

    private void ChangeRangedDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("rangedDamage", amountToAdd);
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
