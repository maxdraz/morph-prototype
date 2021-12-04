using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Number Set", menuName = "FX Data/Damage Number Set")]
public class DamageNumberSet : ScriptableObject
{
    public DamageNumber PhysicalDamageLow;
    public DamageNumber PhysicalDamageMedium;
    public DamageNumber PhysicalDamageHigh;
}
