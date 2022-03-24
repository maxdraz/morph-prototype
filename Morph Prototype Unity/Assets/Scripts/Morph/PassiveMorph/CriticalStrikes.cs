using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalStrikes : PassiveMorph
{
    private DamageHandler damageHandler;

    [SerializeField] private float criticalStrikeChance;
    [SerializeField] private bool unlockHiddenAttack = true;
    [SerializeField] private float hiddenAttackCriticalStrikeChance;
    [SerializeField] private float hiddenAttackExtraDamage;

    [SerializeField] private bool unlockCoupDeGrace = true;
    [SerializeField] private float coupDeGraceCooldownPeriod;
    bool coupDeGraceOnCooldown = false;
    Timer coupDeGraceTimer;



    private void Start()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        GetComponent<Stats>().globalCritChance += criticalStrikeChance;
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }


    private void Update()
    {
        if (coupDeGraceOnCooldown) 
        {
            coupDeGraceTimer.Update(Time.deltaTime); 
        }

        if (coupDeGraceTimer.JustCompleted) 
        {
            coupDeGraceOnCooldown = false; 
        }
    }


    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        

        if (unlockHiddenAttack)
        {
            if (damageTakenSummary.isStealthAttack)
            {
                damageTakenSummary.DamageTaker.ApplyDamage(new PhysicalDamageData(hiddenAttackExtraDamage), damageHandler);
                
            }
            
        }

        if (unlockCoupDeGrace && coupDeGraceOnCooldown == false)
        {
            //check to see if the target of the attacks is currently stunned, if so this attack is a guaranteed crit (start cooldown)
            coupDeGraceTimer = new Timer(coupDeGraceCooldownPeriod, false);
            coupDeGraceOnCooldown = true;

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
            if (unlockHiddenAttack)
            {

            }

        }

        damageHandler = null;
    }
}
