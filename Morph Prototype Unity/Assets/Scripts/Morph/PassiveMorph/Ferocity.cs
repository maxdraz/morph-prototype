using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferocity : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float meleeDamageStatBonus = 5;
    [SerializeField] private bool unlockSpiritSpihon = true;

    float damageBoostPerStack;

    int currentFerocityStackAmount;
    int maxFerocityStacks = 5;
    [SerializeField] private float stackDuration = 4;

    [SerializeField] private float ferocityAttackSpeedBuffPerStack;
    [SerializeField] private float ferocityMeleeAttackDamageBuffPerStack;
    float totalFerocityAttackSpeedBuff;
    float totalFerocityMeleeAttackDamageBuff;


    [SerializeField] private float spiritSiphonStaminaStealAmount;
    [SerializeField] private float spiritSiphonEnergyStealAmount;


    private void Start()
    {
    }

    

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMeleeDamageStat(meleeDamageStatBonus);

    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeMeleeDamageStat(-meleeDamageStatBonus);
    }

    // implement
    private void ChangeMeleeDamageStat(float amountToAdd)
    {

    }

    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        //should check for whether the attack was a melee attack
        if (damageTakenSummary.PhysicalDamage > 0)
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

        for (int i = 0; i <= currentFerocityStackAmount; i++) 
        {
            totalFerocityAttackSpeedBuff += ferocityAttackSpeedBuffPerStack;
            totalFerocityMeleeAttackDamageBuff += ferocityMeleeAttackDamageBuffPerStack;

            if (i == currentFerocityStackAmount) 
            {
            //send the buff out to buff handler
            }
        }
    }

    IEnumerator DecayFerocityStacks() 
    {
        yield return new WaitForSeconds(stackDuration);
        currentFerocityStackAmount = 0;
        yield return null;
    }

    //Needs to be constantly active on a timer
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
            if (unlockSpiritSpihon)
            {
                //damageHandler.DebuffAboutToBeDealtPreModifier += OnAcidDebuffDealt;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;
            if (unlockSpiritSpihon)
            {
                //damageHandler.DebuffAboutToBeDealtPreModifier -= OnAcidDebuffDealt;
            }

        }

        damageHandler = null;
    }
}
