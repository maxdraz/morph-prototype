using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageTakenSummary
{
    public IDamageType DamageType;

    public bool IsFatalBlow;
    public bool IsCriticalHit;
    
    public float TotalDamage 
        => PhysicalDamage + ChemicalDamage+ ElementalDamage + PoisonDamage+AcidDamage+FireDamage+IceDamage+LightningDamage;

    public float PhysicalDamage;
    public float ChemicalDamage;
    public float ElementalDamage;

    public float PoisonDamage;
    public float AcidDamage;
    
    public float FireDamage;
    public float IceDamage;
    public float LightningDamage;
}
