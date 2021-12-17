using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessAggression : PassiveMorph
{
    [SerializeField] private RadialProjectileSpawner explosionSpawner; 
    private DamageHandler damageHandler;
    [SerializeField] private float meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockExplosiveAnger = true;

    [SerializeField] private float endlessAggressionEnergyGain;
    [SerializeField] private float endlessAggressionStaminaGain;

    CombatResources combatResources;

    bool canGainExplosiveAngerStacks = true;
    int currentExplosiveAngerStacks;
    [SerializeField] private int explosiveAngerStackLimit;
    float explosiveAngerStackDuration = 2;
    [SerializeField] private float explosiveAngerCooldownPeriod;
    

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(meleeDamageStatBonus);
        combatResources = GetComponent<CombatResources>();
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

        if (damageTakenSummary.IsCriticalHit)
        {
            GainStaminaAndEnergy();

            if (unlockExplosiveAnger)
            {
                ExplosiveAnger();
            }
        }
    }

    void GainStaminaAndEnergy()
    {
        float currentEnergyPercentage;
        float currentStaminaPercentage;
        currentEnergyPercentage = combatResources.currentEnergyPoints / combatResources.energyPointsMax;
        currentStaminaPercentage = combatResources.currentStaminaPoints / combatResources.staminaPointsMax;

        float energyGainMultiplier = 1 + (1 - currentEnergyPercentage);
        float staminaGainMultiplier = 1 + (1 - currentStaminaPercentage);

        combatResources.currentEnergyPoints += (endlessAggressionEnergyGain * energyGainMultiplier);
        combatResources.currentStaminaPoints += (endlessAggressionStaminaGain * staminaGainMultiplier);
    }

    void ExplosiveAnger()
    {
        currentExplosiveAngerStacks++;

        if (currentExplosiveAngerStacks >= explosiveAngerStackLimit)
        {
            canGainExplosiveAngerStacks = false;
            StopCoroutine("DecayExplosiveAngerStacks");
            currentExplosiveAngerStacks = 0;
            //ExplosiveAngerExplosion(transform.position, explosiveAngerExplosionRadius);
            explosionSpawner.Spawn(transform);
        }

        else
        {
            StopCoroutine("DecayExplosiveAngerStacks");
            StartCoroutine("DecayExplosiveAngerStacks");
        }
    }

    IEnumerator ExplosiveAngerCooldown() 
    {
        yield return new WaitForSeconds(explosiveAngerCooldownPeriod);

        canGainExplosiveAngerStacks = true;


        yield return null;
    }

    IEnumerator DecayExplosiveAngerStacks() 
    {
        yield return new WaitForSeconds(explosiveAngerStackDuration);

        currentExplosiveAngerStacks--;

        if (currentExplosiveAngerStacks > 0) 
        {
            StartCoroutine("DecayExplosiveAngerStacks");
        }

        yield return null;
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

    private void OnDrawGizmos()
    {
        explosionSpawner.OnDrawGizmos(transform);
    }

    private void OnValidate()
    {
        explosionSpawner.OnValidate();
    }
}
