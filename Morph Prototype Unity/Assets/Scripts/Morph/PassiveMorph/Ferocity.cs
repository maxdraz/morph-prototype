using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferocity : PassiveMorph
{
    static int meleeDamagePrerequisit = 35;
    static int agilityPrerequisit = 25;

    private DamageHandler damageHandler;
    [SerializeField] private int meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockSpiritSpihon = true;

    float damageBoostPerStack;

    int currentFerocityStackAmount;
    int maxFerocityStacks = 5;
    [SerializeField] private float stackDuration = 4;

    [SerializeField] private float ferocityAttackSpeedBuffPerStack = .03f;
    [SerializeField] private float ferocityMeleeAttackDamageBuffPerStack = .05f;
    float totalFerocityAttackSpeedBuff;
    float totalFerocityMeleeAttackDamageBuff;

    [SerializeField] private float spiritSiphonPeriod;
    [SerializeField] private float spiritSiphonRange;
    Timer spiritSiphonTimer;
    [SerializeField] private float spiritSiphonStaminaStealAmount;
    [SerializeField] private float spiritSiphonEnergyStealAmount;

    Stats stats;

    //static Prerequisite[] StatPrerequisits;


    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(meleeDamageStatBonus);

        if (unlockSpiritSpihon)
        {
            spiritSiphonTimer = new Timer(spiritSiphonPeriod, true);
        }
    }

    private void OnDisable()
    {
        stats = GetComponent<Stats>();

        UnsubscribeFromEvents();
        ChangeMeleeDamageStat(-meleeDamageStatBonus);
    }

    // implement
    private void ChangeMeleeDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("melee", amountToAdd);
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

    IEnumerator DecayFerocityStacks() 
    {
        yield return new WaitForSeconds(stackDuration);
        currentFerocityStackAmount = 0;
        yield return null;
    }

    private void Update()
    {
        if (unlockSpiritSpihon) 
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
