using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHeavyAttack : HeavyAttack
{
    public GenericHeavyAttack(float baseDamage, float fortitudeDamage, float staminaCost, float energyCost, float critChance, float attackSpeed, float duration, float nextComboInputWindow,bool canComboIntoLight) 
        : base(baseDamage, fortitudeDamage, staminaCost, energyCost, critChance, attackSpeed, duration, nextComboInputWindow,canComboIntoLight)
    {
        
    }
}
