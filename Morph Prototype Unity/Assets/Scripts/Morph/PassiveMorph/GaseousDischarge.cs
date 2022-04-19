using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaseousDischarge : PassiveMorph
{
    static int chemicalDamagePrerequisit = 25;
    [SerializeField] private GaseousDischargePrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private int chemicalDamageStatBonus = 5;

    [SerializeField]private float poisonGasSpawnRate;
    [SerializeField] private float poisonGasLifeTime;
    [SerializeField] Timer poisonGasSpawnCountdown;
    ObjectPooler gasPooler;
    [SerializeField] GameObject gasCloud;

    [SerializeField] private bool unlockToxicOverflow = true;
    [SerializeField] private float toxicOverflowPoisonDamage;
    [SerializeField] private float toxicOverflowKnockBackForce;

    Stats stats;
    //static Prerequisite[] StatPrerequisits;

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
    void Start()
    {
        poisonGasSpawnCountdown = new Timer(poisonGasSpawnRate, true);
        
    }

    // Update is called once per frame
    void Update()
    {
        //poisonGasSpawnCountdown.CountDown(poisonGasSpawnRate);
        poisonGasSpawnCountdown.Update(Time.deltaTime);

        if (poisonGasSpawnCountdown.JustCompleted) 
        {
            //Debug.Log("Timer completed");
            CreateGasCloud();
        }
    }

    

    private void OnDamageTaken(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PhysicalDamage >= damageHandler.Health.currentHealth / 25)
        {

            //now we apply poison damage and knockback to the source of the damage
            damageTakenSummary.DamageDealer.ApplyDamage(new PoisonDamageData(toxicOverflowPoisonDamage),damageHandler);
            damageTakenSummary.DamageDealer.ApplyDamage(new KnockbackData(toxicOverflowKnockBackForce), damageHandler);



        }
    }

    void CreateGasCloud() 
    {
        GameObject poisonGasCloud = ObjectPooler.Instance.GetOrCreatePooledObject(gasCloud);
        poisonGasCloud.transform.position = transform.position;
        poisonGasCloud.GetComponent<PoisonGasCloud>().lifetime = poisonGasLifeTime;
        poisonGasCloud.GetComponent<PoisonGasCloud>().sourceCreature = this.gameObject;
    }

    

    // implement
    private void ChangeChemicalDamageStat(int amountToAdd)
    {
        stats.FlatStatChange("chemicalDamage", amountToAdd);
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
            if (unlockToxicOverflow)
            {
                damageHandler.DamageHasBeenTaken += OnDamageTaken;
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            if (unlockToxicOverflow)
            {
                damageHandler.DamageHasBeenTaken -= OnDamageTaken;
            }

        }

        damageHandler = null;
    }
}
