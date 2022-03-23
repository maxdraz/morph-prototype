using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightenedSenses : PassiveMorph
{
    static int perceptionPrerequisit = 300;

    private DamageHandler damageHandler;
    bool heightenedSensesCountingDown;
    [SerializeField] private float heightenedSensesCountdownDuration = 3;

    [SerializeField] private bool unlockEverReady = true;
    [SerializeField] private float everReadyPerceptionModifier = 2.5f;
    [SerializeField] private float everReadyStealthModifier = 2.5f;

    [SerializeField] private bool unlockAimedShots = true;
    [SerializeField] private float aimedShotsExtraDamageModifier = .3f;


    Stats stats;
    Velocity velo;
    Perception perception;

    //static Prerequisite[] StatPrerequisits;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        velo = GetComponentInParent<Velocity>();
        perception = GetComponent<Perception>();

        StartCoroutine(AssignDamageHandlerCoroutine());

        if (unlockEverReady) 
        {
            EverReady();
        }
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();
        velo = GetComponentInParent<Velocity>();
        perception = GetComponent<Perception>();

        UnsubscribeFromEvents();
    }

    private void Update()
    {
        if (velo.CurrentVelocity.magnitude == 0)
        {
            StartCoroutine("HeightenedSensesCountdown");
            heightenedSensesCountingDown = true;
        }

        else
        {
            ChangePerception(-1);

            if (heightenedSensesCountingDown)
            {
                StopCoroutine("HeightenedSensesCountdown");
                heightenedSensesCountingDown = false;
            }
        }
    }

    IEnumerator HeightenedSensesCountdown()
    {
        yield return new WaitForSeconds(heightenedSensesCountdownDuration);


        ChangePerception(1);

        yield return null;
    }

    private void ChangePerception(float amount)
    {
        perception.perceptionModifier += amount;
    }

    private void EverReady()
    {
        int stealthToAdd = (int)(stats.intelligence * everReadyStealthModifier);
        int perceptionToAdd = (int) (stats.intelligence * everReadyPerceptionModifier);


        stats.FlatStatChange("perception", perceptionToAdd);
        stats.FlatStatChange("stealth", stealthToAdd);
    }

    private void OnDamageAboutToBeDealt(in DamageTakenSummary damageTakenSummary)
    {
        //if (damageTakenSummary.isRangedAttack)
        {
            float aimedShotsDamage = Mathf.Sqrt(perception.CurrentPerception) * aimedShotsExtraDamageModifier;
            damageTakenSummary.DamageTaker.ApplyDamage(new PhysicalDamageData(aimedShotsDamage), damageHandler);
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
            if (unlockAimedShots) 
            {
                damageHandler.DamageHasBeenDealt += OnDamageAboutToBeDealt;

            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {

            if (unlockAimedShots)
            {
                damageHandler.DamageHasBeenDealt -= OnDamageAboutToBeDealt;

            }
        }

        damageHandler = null;
    }
}
