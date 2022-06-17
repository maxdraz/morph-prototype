using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightenedSenses : PassiveMorph
{
    static int perceptionPrerequisit = 300;

    private bool heightenedSensesCountingDown;
    [SerializeField] private float heightenedSensesCountdownDuration = 3;

    [SerializeField] private bool unlockEverReady;
    [SerializeField] private float everReadyPerceptionModifier = 2.5f;
    [SerializeField] private float everReadyStealthModifier = 2.5f;

    [SerializeField] private bool unlockAimedShots;
    [SerializeField] private float aimedShotsExtraDamageModifier = .3f;

    private Velocity velo;
    private Perception perception;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        velo = GetComponentInParent<Velocity>();
        perception = GetComponent<Perception>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        if (unlockEverReady) 
        {
            ApplyEverReady();
        }
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        if (unlockEverReady) 
        {
            ApplyEverReady(false);
        }
    }

    public void UnlockSecondary(string name)
    {
        if (name == "EverReady")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockEverReady = true;
        }

        if (name == "AimedShots")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockAimedShots = true;
        }
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

    private void ApplyEverReady(bool shouldApply = true)
    {
        var sign = shouldApply ? 1 : -1;
        int stealthToAdd = (int)(stats.intelligence * everReadyStealthModifier);
        int perceptionToAdd = (int) (stats.intelligence * everReadyPerceptionModifier);


        stats.FlatStatChange("perception", sign * perceptionToAdd);
        stats.FlatStatChange("stealth", sign * stealthToAdd);
    }

    private void OnDamageAboutToBeDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.isRangedAttack)
        {
            float aimedShotsDamage = Mathf.Sqrt(perception.CurrentPerception) * aimedShotsExtraDamageModifier;
            damageTakenSummary.DamageTaker.ApplyDamage(new PhysicalDamageData(aimedShotsDamage), damageHandler);
        }
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        
        if (damageHandler)
        {
            if (unlockAimedShots) 
            {
                damageHandler.DamageHasBeenDealt += OnDamageAboutToBeDealt;
            }
        }
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        
        if (damageHandler)
        {
            if (unlockAimedShots)
            {
                damageHandler.DamageHasBeenDealt -= OnDamageAboutToBeDealt;
            }
        }
    }
}
