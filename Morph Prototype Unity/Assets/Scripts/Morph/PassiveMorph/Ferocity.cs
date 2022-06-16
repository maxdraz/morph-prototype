using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferocity : PassiveMorph
{
    static int meleeDamagePrerequisit = 35;
    static int agilityPrerequisit = 25;

    [SerializeField] private bool unlockSpiritSiphon;

    private float damageBoostPerStack;

    private int currentFerocityStackAmount;
    private int maxFerocityStacks = 5;
    [SerializeField] private float stackDuration = 4;

    [SerializeField] private float ferocityAttackSpeedBuffPerStack = .03f;
    [SerializeField] private float ferocityMeleeAttackDamageBuffPerStack = .05f;
    private float totalFerocityAttackSpeedBuff;
    private float totalFerocityMeleeAttackDamageBuff;

    [SerializeField] private float spiritSiphonPeriod;
    [SerializeField] private float spiritSiphonRange;
    private Timer spiritSiphonTimer;
    [SerializeField] private float spiritSiphonStaminaStealAmount;
    [SerializeField] private float spiritSiphonEnergyStealAmount;

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ModifyStats(true);
        
        if (unlockSpiritSiphon)
        {
            spiritSiphonTimer = new Timer(spiritSiphonPeriod, true);
        }
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ModifyStats(false);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "SpiritSiphon")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockSpiritSiphon = true;
        }
    }

    // implement
    private void ModifyStats(bool AddToStat)
    {
        if (stats != null)
        {
            if (statsToModify.Length > 0)
            {
                for (int i = 0; i <= statsToModify.Length - 1; i++)
                {
                    if (AddToStat)
                    {
                        Debug.Log(GetType().Name + " is adding" + statsToModify[i].value + " to " + statsToModify[i].stat);
                        stats.FlatStatChange(statsToModify[i].stat.ToString(), statsToModify[i].value);
                    }
                    else
                    {
                        Debug.Log(GetType().Name + " is removing" + statsToModify[i].value + " from " + statsToModify[i].stat);
                        stats.FlatStatChange(statsToModify[i].stat.ToString(), -statsToModify[i].value);
                    }
                }
            }
        }
    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        //should check for whether the attack was a melee attack
        if (damageTakenSummary.PhysicalDamage > 0 && damageTakenSummary.isMeleeAttack)
        {
            AddFerocityStack();
            StopCoroutine("DecayFerocityStacks");
            StartCoroutine("DecayFerocityStacks");
        }
    }

    private void AddFerocityStack()
    {
        if (currentFerocityStackAmount == maxFerocityStacks)
        {
            SendFerocityBuff();
            return;
        }
        else 
        {
            currentFerocityStackAmount++;
            SendFerocityBuff();
        }
    }

    private void SendFerocityBuff() 
    {
        totalFerocityAttackSpeedBuff = ferocityAttackSpeedBuffPerStack * currentFerocityStackAmount;
        totalFerocityMeleeAttackDamageBuff = ferocityMeleeAttackDamageBuffPerStack * currentFerocityStackAmount;

        //Send this temp buff out to handler, must replace whatever value was already there

    }

    private IEnumerator DecayFerocityStacks() 
    {
        yield return new WaitForSeconds(stackDuration);
        currentFerocityStackAmount = 0;
    }

    private void Update()
    {
        if (unlockSpiritSiphon) 
        {
            spiritSiphonTimer.Update(Time.deltaTime);

            if (spiritSiphonTimer.JustCompleted) 
            {
                SpiritSiphon(transform.position,spiritSiphonRange);
            }
        }
    }

    private void SpiritSiphon(Vector3 center, float radius)
    {
        
         Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<Stats>() == true) 
            {
                DamageHandler enemyDamageHandler = hitCollider.GetComponent<DamageHandler>();

                if (hitCollider.GetComponent<CombatResources>().currentStaminaPoints > GetComponent<CombatResources>().currentStaminaPoints) 
                { 
                    enemyDamageHandler.ApplyDamage(new StaminaStealData(spiritSiphonStaminaStealAmount),enemyDamageHandler);
                }

                if (hitCollider.GetComponent<CombatResources>().currentEnergyPoints > GetComponent<CombatResources>().currentEnergyPoints)
                {
                    enemyDamageHandler.ApplyDamage(new EnergyStealData(spiritSiphonEnergyStealAmount), enemyDamageHandler);
                }
            }
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
        }
    }
}
