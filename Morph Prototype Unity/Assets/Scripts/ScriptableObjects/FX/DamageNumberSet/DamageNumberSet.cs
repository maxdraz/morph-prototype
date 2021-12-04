using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Number Set", menuName = "FX Data/Damage Number Set")]
public class DamageNumberSet : ScriptableObject
{
    [Header("Physical Damage")]
    public DamageNumber PhysicalDamageLow;
    public DamageNumber PhysicalDamageMedium;
    public DamageNumber PhysicalDamageHigh;
    [Header("Chemical Damage")]
    public DamageNumber PoisonDamage;
    public DamageNumber AcidDamage;
    [Header("Elemental Damage")]
    public DamageNumber FireDamage;
    public DamageNumber IceDamage;
    public DamageNumber LightningDamage;
    
}
