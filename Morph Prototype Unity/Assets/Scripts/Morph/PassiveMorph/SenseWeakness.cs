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

    [SerializeField] private bool unlockKillerConsumption = true;
    [SerializeField] private float killerConsumptionPercentHeal = .2f;


    Health health;

    static Prerequisite[] BasePrerequisits = new Prerequisite[2]
    {

        new Prerequisite("meleeDamage", meleeDamagePrerequisit),
        new Prerequisite("agility", agilityPrerequisit),
    };

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



    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        //if (damageTakenSummary.isHeavyAttack)
        {
            float enemyHealthPercentage = damageTakenSummary.DamageTaker.GetComponent<Health>().CurrentHealthAsPercentage;

            if (enemyHealthPercentage < 50)

            {
                //gain critchance - bonusCritChance for senseWeaknessDuration
            }
            else 

            {
                //gain agility - bonusAgility for senseWeaknessDuration
            }

            if (unlockKillerConsumption && damageTakenSummary.isMortalBlow)
            {
                health.AddPercentHP(killerConsumptionPercentHeal);
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
