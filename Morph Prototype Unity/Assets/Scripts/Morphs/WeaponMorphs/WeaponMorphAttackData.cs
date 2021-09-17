using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Morph/WeaponMorph/WeaponMorphAttackData")]
public class WeaponMorphAttackData : ScriptableObject
{
    public float baseDamage;
    public float fortitudeDamage;
    public float staminaCost;
    public float energyCost;
    public float critChance;
    public float attackSpeed;
}
