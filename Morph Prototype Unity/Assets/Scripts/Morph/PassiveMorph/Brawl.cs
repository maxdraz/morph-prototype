using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawl : PassiveMorph
{
    //[SerializeField] private BrawlPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private int meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockRage = true;
    [SerializeField] private float bonusAttackSpeed = .06f;


    Stats stats;
    Health health;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        health = GetComponent<Health>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(meleeDamageStatBonus);
        
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();
        health = GetComponent<Health>();

        UnsubscribeFromEvents();
        ChangeMeleeDamageStat(-meleeDamageStatBonus);
    }

    // implement
    private void ChangeMeleeDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("meleeDamage",amountToAdd);
    }

    private void Update()
    {
        if (unlockRage) 
        {
            if (health.CurrentHealthAsPercentage <= 30) 
            {
                //add bonusAttackSpeed to attackhandler
            }

            if (health.CurrentHealthAsPercentage <= 20)
            {
                //add bonusAttackSpeed to attackhandler
            }

            if (health.CurrentHealthAsPercentage <= 10)
            {
                //add bonusAttackSpeed to attackhandler
            }
        }
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
