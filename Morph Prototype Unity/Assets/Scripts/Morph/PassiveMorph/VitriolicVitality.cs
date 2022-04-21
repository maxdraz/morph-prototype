using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitriolicVitality : PassiveMorph
{
    static int chemicalDamagePrerequisit = 30;
    static int toughnessPrerequisit = 30;
    static int fortitudePrerequisit = 30;


    private DamageHandler damageHandler;
    [SerializeField] private int chemicalDamageStatBonus = 5;
    [SerializeField] private float healingPercentageBonus;
    Timer bonusHealingTimer;
    bool bonusHealingTimerCountingDown;
    [SerializeField] private float bonusHealingTimerDuration;

    [SerializeField] private bool unlockVenomousVigor = true;
    [SerializeField] private int venomousVigorHealingDuration;

    [SerializeField] private bool unlockToxicFocus = true;


    Stats stats;

    //public Prerequisite[] StatPrerequisits;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeChemicalDamageStat(chemicalDamageStatBonus);

    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeChemicalDamageStat(-chemicalDamageStatBonus);
    }

    // implement
    private void ChangeChemicalDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("chemicalDamage", amountToAdd);
    }


    // Update is called once per frame
    void Update()
    {
        if (bonusHealingTimerCountingDown) 
        {
            bonusHealingTimer.Update(Time.deltaTime) ;
        }

        if (bonusHealingTimer.JustCompleted) 
        {
            bonusHealingTimerCountingDown = false;
            RemoveHealingBonus();
        }
    }

    void AddHealingBonus() 
    {
        damageHandler.Health.healingPercentageBonus += healingPercentageBonus;
    }

    void RemoveHealingBonus()
    {
        damageHandler.Health.healingPercentageBonus -= healingPercentageBonus;
    }

    private void OnDamageAboutToBeDealt(ref IDamageType damageType) 
    {
        //if the attack was a heavy attack from a weapon morph, add poison damage = 30% of the physical damage before resistances
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PoisonDamage > 0)
        {
            //This timer should restart every time a poison damage tick has been dealt to the target
            bonusHealingTimer = new Timer(bonusHealingTimerDuration, false);
            bonusHealingTimerCountingDown = true;
            AddHealingBonus();

            if (unlockVenomousVigor) 
            {
                //This hp should be added when a target has had poison damage added to their stack
                damageHandler.Health.HealOverTime(damageTakenSummary.PoisonDamage / 2f, venomousVigorHealingDuration);
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

            if (unlockToxicFocus)
            {
                damageHandler.DamageAboutToBeDealt += OnDamageAboutToBeDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;

            if (unlockToxicFocus)
            {
                damageHandler.DamageAboutToBeDealt -= OnDamageAboutToBeDealt;
            }
        }

        damageHandler = null;
    }
}