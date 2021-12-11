using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEdged : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float hpDrainFraction;
    [SerializeField] private float meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockBloodGuzzler = true;

    float damageBoost;
    float damageBoostFactor = 2;

    private void Start()
    {

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
            if (unlockBloodGuzzler)
            {
                //damageHandler.DebuffAboutToBeDealtPreModifier += OnAcidDebuffDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            if (unlockBloodGuzzler)
            {
                //damageHandler.DebuffAboutToBeDealtPreModifier -= OnAcidDebuffDealt;
            }

        }

        damageHandler = null;
    }
}
