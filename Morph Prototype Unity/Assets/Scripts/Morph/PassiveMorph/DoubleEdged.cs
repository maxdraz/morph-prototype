using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEdged : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float hpDrainFraction;
    [SerializeField] private float meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockBloodGuzzler = true;
    bool bloodGuzzlerReady;

    float damageBoost;
    float damageBoostFactor = 2;

    private void Start()
    {
        bloodGuzzlerReady = true;
    }

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(meleeDamageStatBonus);

    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeMeleeDamageStat(-meleeDamageStatBonus);
    }

    // implement
    private void ChangeMeleeDamageStat(float amountToAdd)
    {

    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PhysicalDamage > 0)
        {
            ReduceHPToBoostDamage();
            damageTakenSummary.DamageTaker.ApplyDamage(new PhysicalDamageData(damageTakenSummary.PhysicalDamage* damageBoost), damageHandler);
        }
        if (unlockBloodGuzzler) 
        {
            if (damageTakenSummary.isMortalBlow && bloodGuzzlerReady) 
            {
                GetComponent<CombatResources>().currentStaminaPoints += damageTakenSummary.PhysicalDamage;
                damageHandler.Health.AddHPOverTime(damageTakenSummary.PhysicalDamage, 10);
                bloodGuzzlerReady = false;
                StartCoroutine("BloodGuzzlerCooldown");
            }
        }
    }

    IEnumerator BloodGuzzlerCooldown() 
    {
        yield return new WaitForSeconds(20);
        bloodGuzzlerReady = true;
        yield return null;
    }

    private void ReduceHPToBoostDamage() 
    {
        float hpToReduce = damageHandler.Health.CurrentHealth * hpDrainFraction;
        damageBoost = GetComponent<Stats>().MaxHealth / hpToReduce;
        damageHandler.Health.SubtractHP(hpToReduce);
        damageBoost *= damageBoostFactor;
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
            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
            
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            

        }

        damageHandler = null;
    }
}
