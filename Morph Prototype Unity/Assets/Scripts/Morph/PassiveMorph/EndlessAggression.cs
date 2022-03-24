using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessAggression : PassiveMorph
{
    static int meleeDamagePrerequisit = 30;
    static int intelligencePrerequisit = 30;
    static int fortitudePrerequisit = 30;

    [SerializeField] private RadialProjectileSpawner explosionSpawner;
    private DamageHandler damageHandler;
    [SerializeField] private float maxStaminaStatBonus = .15f;
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

    //public Prerequisite[] StatPrerequisits;

    private void OnEnable()
    {
        stamina = GetComponent<Stamina>();
        energy = GetComponent<Energy>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMaxStaminaStat(maxStaminaStatBonus);

    }

    private void OnDisable()
    {
        stamina = GetComponent<Stamina>();
        energy = GetComponent<Energy>();

        UnsubscribeFromEvents();
        ChangeMaxStaminaStat(-maxStaminaStatBonus);
    }

    private void ChangeMaxStaminaStat(float maxStaminaBonus)
    {
        stamina.maxStaminaBonus += maxStaminaBonus;
    }

    //private void Update()
    //{
        //if (Input.GetKeyDown("p")) 
        //{
         //   explosionSpawner.Spawn(transform);
        //}
    //}

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {

        if (damageTakenSummary.IsCriticalHit)
        {
            GainStaminaAndEnergy();
        }
    }

    void GainStaminaAndEnergy()
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
