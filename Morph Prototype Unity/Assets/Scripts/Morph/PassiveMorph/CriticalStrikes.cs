using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalStrikes : PassiveMorph
{
    [SerializeField] private float criticalStrikeChance;
    [SerializeField] private bool unlockHiddenAttack;
    [SerializeField] private float hiddenAttackCriticalStrikeChance;
    [SerializeField] private float hiddenAttackExtraDamage;

    [SerializeField] private bool unlockCoupDeGrace;
    [SerializeField] private float coupDeGraceCooldownPeriod;
    private bool coupDeGraceOnCooldown = false;
    private Timer coupDeGraceTimer;
    
    protected override void OnEquip()
    {
        base.OnEquip();
        
        stats.globalCritChance += criticalStrikeChance;
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        stats.globalCritChance -= criticalStrikeChance;
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

    public void UnlockSecondary(string name)
    {
        if (name == "HiddenAttack")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockHiddenAttack = true;
        }

        if (name == "CoupDeGrace")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockCoupDeGrace = true;
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

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;
        }
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            if (unlockHiddenAttack)
            {

            }
        }
    }
}
