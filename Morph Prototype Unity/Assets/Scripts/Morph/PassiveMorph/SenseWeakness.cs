using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseWeakness : PassiveMorph
{
    static int meleeDamagePrerequisit = 35;
    static int agilityPrerequisit = 25;



    private DamageHandler damageHandler;

    [SerializeField] private float bonusCritChance = .2f;
    [SerializeField] private float bonusAgility = .2f;
    private float senseWeaknessDuration = 3;

    [SerializeField] private bool unlockKillerConsumption;
    [SerializeField] private float killerConsumptionPercentHeal = .2f;


    Health health;

    //public Prerequisite[] StatPrerequisits;

    private void OnEnable()
    {
        health = GetComponent<Health>();

        StartCoroutine(AssignDamageHandlerCoroutine());
    }

    private void OnDisable()
    {
        health = GetComponent<Health>();

        UnsubscribeFromEvents();
    }

    public void UnlockSecondary(string name)
    {
        if (name == "KillerConsumption")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockKillerConsumption = true;
        }
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.isHeavyAttack)
        {
            float enemyHealthPercentage = damageTakenSummary.DamageTaker.GetComponent<Health>().CurrentHealthAsPercentage;
            float enemyStaminaPercentage = damageTakenSummary.DamageTaker.GetComponent<Stamina>().CurrentStaminaAsPercentage;

            if (enemyHealthPercentage < 50)

            {
                AddCritChanceForDuration();
            }
            
            if (enemyStaminaPercentage < 50)
            {
                AddAgilityForDuration();
            }

            if (unlockKillerConsumption && damageTakenSummary.isMortalBlow)
            {
                health.AddPercentHP(killerConsumptionPercentHeal);
            }
        }
    }

    private void AddCritChanceForDuration() 
    {
        StopCoroutine("AddCritChance");
        StartCoroutine("AddCritChance"); 
    }

    private void AddAgilityForDuration()
    {
        StopCoroutine("AddAgility");
        StartCoroutine("AddAgility");
    }
    private IEnumerator AddAgility()
    {
        GetComponent<Stats>().FlatStatChange("agility", 10);

        yield return new WaitForSeconds(senseWeaknessDuration);

        GetComponent<Stats>().FlatStatChange("agility", -10);

        yield return null;
    }

    private IEnumerator AddCritChance() 
    {
        GetComponent<Stats>().globalCritChance += 10;

        yield return new WaitForSeconds(senseWeaknessDuration);

        GetComponent<Stats>().globalCritChance -= 10;

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
}
