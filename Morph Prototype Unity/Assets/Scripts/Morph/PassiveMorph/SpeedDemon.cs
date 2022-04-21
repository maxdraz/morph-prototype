using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDemon : PassiveMorph
{
    static int agilityPrerequisit = 40;


    private DamageHandler damageHandler;
    [SerializeField] private int meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockCruelCapacity = true;

    int agilityPerStack;
    int bonusAgility;
    int currentSpeedDemonStacks;
    [SerializeField] private float startingSpeedDemonStackDuration;
    float speedDemonStackDuration;

    int cruelCapacityAgilityThreshold;

    Stats stats;

    //public Prerequisite[] StatPrerequisits;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeAgilityStat(meleeDamageStatBonus);

    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeAgilityStat(-meleeDamageStatBonus);
    }

    // implement
    private void ChangeAgilityStat(int amountToAdd)
    {
        stats.FlatStatChange("agility",amountToAdd);
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
