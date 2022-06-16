using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessAggression : PassiveMorph
{
    private static int meleeDamagePrerequisit = 30;
    private static int intelligencePrerequisit = 30;
    private static int fortitudePrerequisit = 30;


    [SerializeField] private RadialProjectileSpawner explosionSpawner;
    [SerializeField] private float maxStaminaStatBonus = .15f;
    [SerializeField] private bool unlockExplosiveAnger = true;

    [SerializeField] private float endlessAggressionEnergyGain;
    [SerializeField] private float endlessAggressionStaminaGain;

    private Stamina stamina;
    private Energy energy;

    private bool canGainExplosiveAngerStacks = true;
    private int currentExplosiveAngerStacks;
    [SerializeField] private int explosiveAngerStackLimit;
    private float explosiveAngerStackDuration = 2;
    [SerializeField] private float explosiveAngerCooldownPeriod;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        energy = GetComponent<Energy>();
        stamina = GetComponent<Stamina>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ChangeMaxStaminaStat(maxStaminaStatBonus);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ChangeMaxStaminaStat(-maxStaminaStatBonus);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "ExplosiveAnger")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockExplosiveAnger = true;
        }
    }

    private void ChangeMaxStaminaStat(float maxStaminaBonus)
    {
        stamina.maxStaminaBonus += maxStaminaBonus;
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.IsCriticalHit)
        {
            //GainStaminaAndEnergy();
            ExplosiveAnger();
        }
    }

    private void GainStaminaAndEnergy()
    {
        float currentEnergyPercentage = energy.EnergyAsPercentage();
        float currentStaminaPercentage = stamina.CurrentStaminaAsPercentage;

        float energyGainMultiplier = 1 + (1 - currentEnergyPercentage);
        float staminaGainMultiplier = 1 + (1 - currentStaminaPercentage);

        energy.AddEnergy(endlessAggressionEnergyGain * energyGainMultiplier);
        stamina.AddStamina(endlessAggressionStaminaGain * staminaGainMultiplier);

        if (unlockExplosiveAnger) 
        {
            if (currentEnergyPercentage == 100)
            {
                ExplosiveAnger();
            }
        }
    }

    void ExplosiveAnger()
    {
        currentExplosiveAngerStacks++;

        if (currentExplosiveAngerStacks >= explosiveAngerStackLimit)
        {
            canGainExplosiveAngerStacks = false;
            StopCoroutine("ExplosiveAngerCooldown");
            currentExplosiveAngerStacks = 0;
            explosionSpawner.Spawn(transform);
        }

        else
        {
            StopCoroutine("DecayExplosiveAngerStacks");
            StartCoroutine("DecayExplosiveAngerStacks");
        }
    }

    private IEnumerator ExplosiveAngerCooldown() 
    {
        yield return new WaitForSeconds(explosiveAngerCooldownPeriod);

        canGainExplosiveAngerStacks = true;


        yield return null;
    }

    private IEnumerator DecayExplosiveAngerStacks() 
    {
        yield return new WaitForSeconds(explosiveAngerStackDuration);

        currentExplosiveAngerStacks--;

        if (currentExplosiveAngerStacks > 0) 
        {
            StartCoroutine("DecayExplosiveAngerStacks");
        }

        yield return null;
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        
        if (damageHandler)
            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        
        if (damageHandler)
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
    }

    private void OnDrawGizmos()
    {
        explosionSpawner?.OnDrawGizmos(transform);
    }

    private void OnValidate()
    {
        explosionSpawner?.OnValidate();
    }
}
