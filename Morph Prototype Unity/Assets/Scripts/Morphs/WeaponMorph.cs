using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMorph : Morph
{
    // initialized from scriptable obj
    int basicAttackComboLength;
    int heavyAttackComboLength;
    float[] baseDamage;
    float[] fortitudeDamage;
    float[] staminaCosts;
    float[] energyCosts;
    float[] critChance;
    float attackSpeed;
}
