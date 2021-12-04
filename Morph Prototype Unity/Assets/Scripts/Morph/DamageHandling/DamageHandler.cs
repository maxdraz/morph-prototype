using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DamageNumbersPro;

public class DamageHandler : MonoBehaviour
{
    public DamageTakenSummary summary;
    //==============================
    private Stats stats;
    private Health health;
    private Armor armor;
    [SerializeField] private bool isInvincible = true;
    [SerializeField] private DamageNumberSet damageNumberSet;

    public Stats Stats => stats;
    public Health Health => health;
    public Armor Armor => armor;
    public bool IsInvincible => isInvincible;
    
    [SerializeField] private List<Debuff> activeDebuffs;
    private List<DamageHandler> damageTakers;

    
    // public events
    public delegate void DamageAboutToBeTakenHandler(ref IDamageType damageType);
    public event DamageAboutToBeTakenHandler DamageAboutToBeTaken;
    public delegate void DamageHasBeenTakenHandler(in DamageTakenSummary damageSummary);
    public event DamageHasBeenTakenHandler DamageHasBeenTaken;
    public event Action<DamageHandler> DamageTakerAdded;
    public event Action<DamageHandler> DamageTakerRemoved;

    private void Awake()
    {
        stats = GetComponentInParent<Stats>();
        health = GetComponent<Health>();
        armor = GetComponent<Armor>();

        if(!stats) Debug.LogWarning(transform.parent.name +" dmg handler couldnt find stats");
        if(!health) Debug.LogWarning(transform.parent.name +" dmg handler couldnt find health");
    }

    private void Update()
    {
        // tick debuffs
    }

    public void ApplyDamage(IDamageType damage, DamageHandler damageDealer)
    {
        var damageClone = damage.Clone() as IDamageType;
        
        DamageAboutToBeTaken?.Invoke(ref damageClone);

        ResistDamage(ref damageClone, damageDealer, out var damageTakenSummary);
        summary = damageTakenSummary;
        HandleDamageTaken(in damageTakenSummary);
        
        DamageHasBeenTaken?.Invoke(in damageTakenSummary);
    }

    private void HandleDamageTaken(in DamageTakenSummary damageTakenSummary)
    {
        health.SubtractHP(damageTakenSummary.TotalDamage);
        // apply fortitude damage
            // apply relevant status effects ...

        DisplayDamageNumbers(in damageTakenSummary);

    }

    private void DisplayDamageNumbers(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PhysicalDamage > 0)
        {
            if (damageTakenSummary.PhysicalDamage <= 10)
            {
                damageNumberSet.PhysicalDamageLow.CreateNew(damageTakenSummary.PhysicalDamage, transform.position);
            }
            else if (damageTakenSummary.PhysicalDamage <= 15)
            {
                damageNumberSet.PhysicalDamageMedium.CreateNew(damageTakenSummary.PhysicalDamage, transform.position);
            }
            else if (damageTakenSummary.PhysicalDamage > 15)
            {
                damageNumberSet.PhysicalDamageHigh.CreateNew(damageTakenSummary.PhysicalDamage, transform.position);
            }
        }

        //other damage types
        if(damageTakenSummary.PoisonDamage > 0)
            damageNumberSet.PoisonDamage.CreateNew(damageTakenSummary.PoisonDamage, transform.position);
        
        if(damageTakenSummary.AcidDamage > 0)
            damageNumberSet.AcidDamage.CreateNew(damageTakenSummary.AcidDamage, transform.position);
        
        if(damageTakenSummary.FireDamage > 0)
            damageNumberSet.FireDamage.CreateNew(damageTakenSummary.FireDamage, transform.position);
        
        if(damageTakenSummary.IceDamage > 0)
            damageNumberSet.IceDamage.CreateNew(damageTakenSummary.IceDamage, transform.position);
        
        if(damageTakenSummary.LightningDamage > 0)
            damageNumberSet.LightningDamage.CreateNew(damageTakenSummary.LightningDamage, transform.position);
        
    }

    public void ApplyDebuff(Debuff debuff)
    {
        // make copy of debuff
        // broadcast DebuffAboutToBeApplied ( debuff copy)
    }
    private void ResistDamage(ref IDamageType damageType, DamageHandler damageDealer, out DamageTakenSummary damageTakenSummary)
    {
        damageTakenSummary = new DamageTakenSummary
        {
            DamageType = damageType,
            DamageTaker = this,
            DamageDealer = damageDealer,
            
            PhysicalDamage = HandlePhysicalDamage(ref damageType),
            
            PoisonDamage = HandlePoisonDamage(ref damageType),
            AcidDamage = HandleAcidDamage(ref damageType),
            
            FireDamage = HandleFireDamage(ref damageType),
            IceDamage = HandleIceDamage(ref damageType),
            LightningDamage = HandleLightningDamage(ref damageType),
            
            FortitudeDamage = HandleFortitudeDamage(ref damageType)
        };

        damageTakenSummary.IsFatalBlow = health.WillDieFromThisDamage(damageTakenSummary.TotalDamage);
    }

    private float HandlePhysicalDamage(ref IDamageType damageType)
    {
        if (damageType is IPhysicalDamage physicalDamage)
        {
            bool isPiercing = damageType is IPiercingDamage;
            if (isPiercing)
            {
                print("piercing damage dealt");
            }
            bool isImpact = damageType is IImpactDamage;

            return DamageFormulas.PhysicalDamageResist(
                physicalDamage.PhysicalDamageDealt,
                isPiercing,
                stats.ToughnessModifier,
                0,
                armor.HasArmor,
                0);
        }

        return 0f;
    }
    private float HandleFireDamage(ref IDamageType damageType)
    {
        if (damageType is IFireDamage fireDamage)
        {
            return fireDamage.FireDamage;
        }
        return 0f;
    }
    private float HandleIceDamage(ref IDamageType damageType)
    {
        if (damageType is IIceDamage iceDamage)
        {
            return iceDamage.IceDamage;
        }
        return 0f;
    }
    private float HandleLightningDamage(ref IDamageType damageType)
    {
        if (damageType is ILightningDamage lightningDamage)
        {
            return lightningDamage.LightningDamage;
        }
        return 0f;
    }
    private float HandlePoisonDamage(ref IDamageType damageType)
    {
        if (damageType is IPoisonDamage poisonDamage)
        {
            return poisonDamage.PoisonDamage;
        }
        return 0f;
    }
    private float HandleAcidDamage(ref IDamageType damageType)
    {
        if (damageType is IAcidDamage acidDamage)
        {
            return acidDamage.AcidDamage;
        }
        return 0f;
    }
    private float HandleFortitudeDamage(ref IDamageType damageType)
    {
        return 0f;
    }
}
