using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DamageNumbersPro;

public enum AttackType{ Melee,Ranged, AOE}

public class DamageHandler : MonoBehaviour
{
    private Stats stats;
    private Health health;
    private Armor armor;
    private Stamina stamina;
    private Rigidbody parentRigidbody;
    private Fortitude fortitude;
    [SerializeField] private bool isInvincible = true;
    [SerializeField] private DamageNumberSet damageNumberSet;

    public Stats Stats => stats;
    public Health Health => health;
    public Armor Armor => armor;
    public bool IsInvincible => isInvincible;
    private float acidifiedDebuff;


    // public events
    public delegate void DebuffAboutToBeTakenPreModifierHandler(ref IDamageType damageType, DamageHandler damageDealer);
    public event DebuffAboutToBeTakenPreModifierHandler DebuffAboutToBeTakenPreModifier;
    public delegate void DebuffAboutToBeDealtPreModifierHandler(ref IDamageType damageType, DamageHandler damageTaker);
    public event DebuffAboutToBeDealtPreModifierHandler DebuffAboutToBeDealtPreModifier;
    public delegate void DebuffAboutToBeTakenPostModifierHandler(ref IDamageType damageType, DamageHandler damageDealer);
    public event DebuffAboutToBeTakenPostModifierHandler DebuffAboutToBeTakenPostModifier;
    public delegate void DebuffAboutToBeDealtPostModifierHandler(ref IDamageType damageType, DamageHandler damageTaker);
    public event DebuffAboutToBeDealtPostModifierHandler DebuffAboutToBeDealtPostModifier;
    
    public delegate void DamageAboutToBeTakenHandler(ref IDamageType damageType);
    public event DamageAboutToBeTakenHandler DamageAboutToBeTaken;
    public delegate void DamageHasBeenTakenHandler(in DamageTakenSummary damageSummary);
    public event DamageHasBeenTakenHandler DamageHasBeenTaken;
    public delegate void DamageAboutToBeDealtHandler(ref IDamageType damageType);
    public event DamageAboutToBeDealtHandler DamageAboutToBeDealt;
    public delegate void DamageHasBeenDealtHandler(in DamageTakenSummary damageSummary);
    public event DamageHasBeenDealtHandler DamageHasBeenDealt;
    public event Action<DamageHandler> DamageTakerAdded;
    public event Action<DamageHandler> DamageTakerRemoved;

    private void Awake()
    {
        stats = GetComponentInParent<Stats>();
        health = GetComponent<Health>();
        armor = GetComponent<Armor>();
        stamina = GetComponent<Stamina>();
        parentRigidbody = GetComponentInParent<Rigidbody>();
        fortitude = GetComponent<Fortitude>();

        if (!stats) Debug.LogWarning(transform.parent.name +" dmg handler couldnt find stats");
        if(!health) Debug.LogWarning(transform.parent.name +" dmg handler couldnt find health");
    }

    public void ApplyDamage(IDamageType damageType, DamageHandler damageDealer)
    {
        var damageClone = damageType.Clone() as IDamageType;
        
        // calculate miss
            // miss chance = 1 - currentPerception/ maxPerception
                // if aoe miss chance / 2
            // roll 
                // if miss 
                    // OnAttackerMiss()
                    // return;
                    
        // calculate dodge
            // roll dodgeChance
                // if aoe dodgeChance / 2
                // Dodge()
        
        damageDealer.DamageAboutToBeDealt?.Invoke(ref damageClone);
        DamageAboutToBeTaken?.Invoke(ref damageClone);

        ResistDamage(ref damageClone, damageDealer, out var damageTakenSummary);
        HandleDamageTaken(in damageTakenSummary);
        
        damageDealer.DamageHasBeenDealt?.Invoke(in damageTakenSummary);
        DamageHasBeenTaken?.Invoke(in damageTakenSummary);
    }
    

    private void HandleDamageTaken(in DamageTakenSummary damageTakenSummary)
    {
        health.SubtractHP(damageTakenSummary.TotalDamage);
        // apply fortitude damage
        ApplyFortitudeDamage(damageTakenSummary.FortitudeDamageData);
        // apply relevant status effects ...
        stamina.SubtractStamina(damageTakenSummary.StaminaDrained);
            // fortitude check for mortal blow 
        ApplyKnockback(in damageTakenSummary);
        ApplyKnockup(in damageTakenSummary);
        ApplyPullTowards(in damageTakenSummary);

        VisualizeDamage(in damageTakenSummary);

    }

    private void ApplyKnockback(in DamageTakenSummary damageTakenSummary)
    {
        if(!parentRigidbody) return;   
        if(damageTakenSummary.KnockbackForce <= 0) return;

        var forceDirectionNormalized = (transform.position - damageTakenSummary.DamageDealer.transform.position).normalized;
        parentRigidbody.AddForce(forceDirectionNormalized * damageTakenSummary.KnockbackForce, ForceMode.Impulse);
        Debug.Log("applying " + damageTakenSummary.KnockbackForce + " knockback" + " to " + transform.name);

    }

    private void ApplyPullTowards(in DamageTakenSummary damageTakenSummary)
    {
        if (!parentRigidbody) return;
        if (damageTakenSummary.PullForce <= 0) return;

        var forceDirectionNormalized = ((transform.position - damageTakenSummary.DamageDealer.transform.position) * -1).normalized;
        parentRigidbody.AddForce(forceDirectionNormalized * damageTakenSummary.PullForce, ForceMode.Impulse);
        Debug.Log("applying " + damageTakenSummary.PullForce + " pullforce" + " to " + transform.name);
    }

    private void ApplyKnockup(in DamageTakenSummary damageTakenSummary)
    {
        if (!parentRigidbody) return;
        if (damageTakenSummary.KnockupForce <= 0) return;

        parentRigidbody.AddForce(Vector3.up * damageTakenSummary.KnockupForce, ForceMode.Impulse);
        Debug.Log("applying " + damageTakenSummary.KnockupForce + " knockup" + " to " + transform.name);

    }

    private void ApplyFortitudeDamage(FortitudeDamageData damageTakenSummary)
    {
        if (!fortitude) return;
        //Debug.Log("has fortitude script");
        //Debug.Log("data has: " + damageTakenSummary.FortitudeDamage + " with an effect of " + damageTakenSummary.StatusEffect + " and a duration of " + damageTakenSummary.Duration);
        if (damageTakenSummary.FortitudeDamage <= 0) return;
        

        float fortitudeDamagePercentReduction = damageTakenSummary.FortitudeDamage *= fortitude.fortitudeDamagePercentResistance;
        damageTakenSummary.FortitudeDamage -= fortitudeDamagePercentReduction;
        damageTakenSummary.FortitudeDamage -= fortitude.fortitudeDamageFlatResistance;

        float statusEffectDurationReduction = damageTakenSummary.Duration *= fortitude.statusEffectDurationReduction;
        damageTakenSummary.FortitudeDamage -= statusEffectDurationReduction;
        

        fortitude.ApplyFortitudeDamage(damageTakenSummary);

        //Debug.Log("applying " + damageTakenSummary.FortitudeDamage + " FortitudeDamage" + " to " + transform.name +
        //    " with an effect of " + damageTakenSummary.StatusEffect + " and a duration of " + damageTakenSummary.Duration);

    }

    private void VisualizeDamage(in DamageTakenSummary damageTakenSummary)
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
        
        // lifesteal
        if (damageTakenSummary.DamageType is LifeStealData lifeStealData)
        {
            if (lifeStealData.LifestealFXData)
            {
                var lifestealParticles = GameplayStatics.SpawnParticleSystem(
                    lifeStealData.LifestealFXData.LifestealParticles,
                    transform);

                if (lifestealParticles)
                {
                    var attractor = lifestealParticles.GetComponentInChildren<particleAttractorLinear>();
                    if (attractor) attractor.target = damageTakenSummary.DamageDealer.transform;
                }
                
            }
        }
        
        //stamina drain 
        if (damageTakenSummary.StaminaDrained > 0)
        {
            damageNumberSet.StaminaDrain.CreateNew(damageTakenSummary.StaminaDrained, transform.position);
        }
        
    }

    public void ApplyDebuff(IDamageType damageType, DamageHandler damageDealer)
    {
        var damageTypeClone = damageType.Clone() as IDamageType;
        damageDealer.DebuffAboutToBeDealtPreModifier?.Invoke(ref damageTypeClone, this);
        DebuffAboutToBeTakenPreModifier?.Invoke(ref damageTypeClone, damageDealer);
        damageDealer.DebuffAboutToBeDealtPostModifier?.Invoke(ref damageTypeClone, this);
        DebuffAboutToBeTakenPostModifier?.Invoke(ref damageTypeClone, damageDealer);
    }
    private void ResistDamage(ref IDamageType damageType, DamageHandler damageDealer, out DamageTakenSummary damageTakenSummary)
    {
        damageTakenSummary = new DamageTakenSummary
        {
            DamageType = damageType,
            DamageTaker = this,
            DamageDealer = damageDealer,

            PhysicalDamage = HandlePhysicalDamage(ref damageType),
            LifeStealDamage = HandleLifestealDamage(ref damageType),

            PoisonDamage = HandlePoisonDamage(ref damageType),
            AcidDamage = HandleAcidDamage(ref damageType),
            FireDamage = HandleFireDamage(ref damageType),
            IceDamage = HandleIceDamage(ref damageType),
            LightningDamage = HandleLightningDamage(ref damageType),
            StaminaDrained = HandlerStaminaDrain(ref damageType),
            //FortitudeDamage = HandleFortitudeDamage(ref damageType),
            KnockbackForce = HandleKnockback(ref damageType),
            KnockupForce = HandleKnockup(ref damageType),
            PullForce = HandlePullTowards(ref damageType)

        };

        damageTakenSummary.IsFatalBlow = health.WillDieFromThisDamage(damageTakenSummary.TotalDamage);
        
    }

    private float HandlePhysicalDamage(ref IDamageType damageType)
    {
        if (damageType is IPhysicalDamage physicalDamage)
        {
            bool isPiercing = damageType is IPiercingDamage;
            
            bool isImpact = damageType is IImpactDamage;

            return DamageFormulas.PhysicalDamageResist(
                physicalDamage.PhysicalDamageDealt,
                isPiercing,
                stats.ToughnessModifier,
                0,
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
    
    private void HandleFortitudeDamage(ref IDamageType damageType)
    {
        if (damageType is IFortitudeDamage fortitudeDamage)
        {
           // Resists
        }
    }
    
    private float HandleLifestealDamage(ref IDamageType damageType)
    {
        if (damageType is ILifestealDamage lifestealDamage)
        {
            return lifestealDamage.LifestealAmount;
        }
        return 0f;
    }

    private float HandlerStaminaDrain(ref IDamageType damageType)
    {
        if (damageType is IStaminaDrain staminaDrain)
        {
            return staminaDrain.StaminaToDrain;
        }

        return 0;
    }

    private float HandleKnockback(ref IDamageType damageType)
    {
        if (damageType is IKnockback knockback)
        {
            return knockback.KnockbackForce;
        }

        return 0;
    }

    private float HandleKnockup(ref IDamageType damageType)
    {
        if (damageType is IKnockUp knockup)
        {
            return knockup.KnockupForce;
        }

        return 0;
    }

    private float HandlePullTowards(ref IDamageType damageType)
    {
        if (damageType is IPullTowards pullTowards)
        {
            return pullTowards.PullForce;
        }

        return 0;
    }
}
