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

    Stamina stamina;
    Energy energy;
        
    bool canGainExplosiveAngerStacks = true;
    int currentExplosiveAngerStacks;
    [SerializeField] private int explosiveAngerStackLimit;
    float explosiveAngerStackDuration = 2;
    [SerializeField] private float explosiveAngerCooldownPeriod;
    

    private void OnEnable()
    {
        stamina = GetComponent<Stamina>();
        energy = GetComponent<Energy>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(meleeDamageStatBonus);
    }

    private void OnDisable()
    {
        stamina = GetComponent<Stamina>();
        energy = GetComponent<Energy>();

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
        float currentEnergyPercentage = energy.EnergyAsPercentage();
        float currentStaminaPercentage = stamina.StaminaAsPercentage();

        float energyGainMultiplier = 1 + (1 - currentEnergyPercentage);
        float staminaGainMultiplier = 1 + (1 - currentStaminaPercentage);

        energy.AddEnergy(endlessAggressionEnergyGain * energyGainMultiplier);
        stamina.AddStamina(endlessAggressionStaminaGain * staminaGainMultiplier);
    }

    void ExplosiveAnger()
    {
        currentExplosiveAngerStacks++;

        if (currentExplosiveAngerStacks >= explosiveAngerStackLimit)
        {
            canGainExplosiveAngerStacks = false;
            StopCoroutine("ExplosiveAngerCooldown");
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
