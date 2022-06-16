using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaseousDischarge : PassiveMorph
{
    static int chemicalDamagePrerequisit = 25;

    [SerializeField]private float poisonGasSpawnRate;
    [SerializeField] private float poisonGasLifeTime;
    [SerializeField] Timer poisonGasSpawnCountdown;
    private ObjectPooler gasPooler;
    [SerializeField] private GameObject gasCloud;

    [SerializeField] private bool unlockToxicOverflow;
    [SerializeField] private float toxicOverflowPoisonDamage;
    [SerializeField] private float toxicOverflowKnockBackForce;

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ModifyStats(true);
        
        poisonGasSpawnCountdown = new Timer(poisonGasSpawnRate, true);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ModifyStats(false);
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

    public void UnlockSecondary(string name)
    {
        if (name == "ToxicOverflow")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockToxicOverflow = true;
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



    // If the bool AddToStat is set to positive it will add to the stats, if negative it will remove from the stats
    void ModifyStats(bool AddToStat)
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

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        
        if (damageHandler)
        {
            if (unlockToxicOverflow)
            {
                damageHandler.DamageHasBeenTaken += OnDamageTaken;
            }
        }
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        
        if (damageHandler)
        {
            if (unlockToxicOverflow)
            {
                damageHandler.DamageHasBeenTaken -= OnDamageTaken;
            }
        }
    }
}
