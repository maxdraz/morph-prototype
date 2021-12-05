using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageTakenSummary
{
    public IDamageType DamageType;
    public DamageHandler DamageTaker;
    public DamageHandler DamageDealer;

    public bool IsFatalBlow;
    public bool IsCriticalHit;
    public bool isMortalBlow;
    
    public float TotalDamage 
        => PhysicalDamage + TotalChemicalDamage + TotalElementalDamage + LifeStealDamage;

    public float TotalChemicalDamage
        => AcidDamage + PoisonDamage;

    public float TotalElementalDamage
        => IceDamage + FireDamage + LightningDamage;

    public float PhysicalDamage;
    public float LifeStealDamage;

    public float PoisonDamage;
    public float AcidDamage;
    
    public float FireDamage;
    public float IceDamage;
    public float LightningDamage;

    public float StaminaDrained;
    public float FortitudeDamage;
}
