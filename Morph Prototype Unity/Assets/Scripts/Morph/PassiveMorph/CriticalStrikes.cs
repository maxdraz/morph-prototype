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


    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        //universal criticalStrikeChance += criticalStrikeChance
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


    private void OnDamageAboutToBeDealt(ref IDamageType damageType)
    {
        

        if (unlockHiddenAttack)
        {
            //check to see if you are performing a stealth attack, if so add extra damage and crit chance

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
            damageHandler.DamageAboutToBeDealt += OnDamageAboutToBeDealt;
            
            
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageAboutToBeDealt -= OnDamageAboutToBeDealt;
            if (unlockHiddenAttack)
            {

            }

        }

        damageHandler = null;
    }
}
