using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawl : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private int meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockRage = true;
    [SerializeField] private float bonusAttackSpeed = 5;


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
                //need stats script to have a function for recieving stats
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
