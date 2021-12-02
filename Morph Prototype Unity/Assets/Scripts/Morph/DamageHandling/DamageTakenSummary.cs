using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenSummary
{
    public IDamageType DamageType;

    public bool IsFatalBlow;
    public bool IsCriticalHit;
    
    public float TotalDamage;

    public float PhysicalDamage;
    public float ChemicalDamage;
    public float ElementalDamage;

    public float PoisonDamage;
    public float AcidDamage;
    
    public float FireDamage;
    public float IceDamage;
    public float LightningDamage;
}
