using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDemon : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockCruelCapacity = true;

    int agilityPerStack;
    int bonusAgility;
    int currentSpeedDemonStacks;
    [SerializeField] private float startingSpeedDemonStackDuration;
    float speedDemonStackDuration;

    int cruelCapacityAgilityThreshold;

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

        if (damageTakenSummary.IsCriticalHit)
        {
            AddSpeedDemonStack();
        }
    }

    void AddSpeedDemonStack() 
    {
        currentSpeedDemonStacks++;
        bonusAgility = currentSpeedDemonStacks * agilityPerStack;
        StopCoroutine("DecaySpeedDemonStacks");
        StartCoroutine("DecaySpeedDemonStacks");
    }

    void RemoveSpeedDemonStack()
    {
        currentSpeedDemonStacks--;
        bonusAgility = currentSpeedDemonStacks * agilityPerStack;

        if (currentSpeedDemonStacks > 0) 
        {
            StartCoroutine("DecaySpeedDemonStacks");
        }
    }

    IEnumerator DecaySpeedDemonStacks() 
    {

        if (currentSpeedDemonStacks > 1)
        {
            speedDemonStackDuration = startingSpeedDemonStackDuration * Mathf.Pow(.9f, currentSpeedDemonStacks);
        }
        else 
        {
            speedDemonStackDuration = startingSpeedDemonStackDuration;
        }

        yield return new WaitForSeconds(speedDemonStackDuration);

        RemoveSpeedDemonStack();

        yield return null;
    }

    void CruelCapacity() 
    {
        //Need to reduce all cooldowns and reduce all energy costs, when modified agility is above a certain threshold
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
            if (unlockCruelCapacity)
            {
                //need to subscribe to something which can hold the buffs
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            if (unlockCruelCapacity)
            {
                //need to subscribe to something which can hold the buffs
            }

        }

        damageHandler = null;
    }
}