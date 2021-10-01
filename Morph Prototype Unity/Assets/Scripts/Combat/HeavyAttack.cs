using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : Attack
{
    public bool CanComboIntoLight;

    public HeavyAttack(float baseDamage, float fortitudeDamage, float staminaCost, float energyCost, float critChance, float attackSpeed,float duration, float nextComboInputWindow, bool canComboIntoLight) 
        : base(baseDamage, fortitudeDamage, staminaCost, energyCost, critChance, attackSpeed, duration, nextComboInputWindow)
    {
        this.CanComboIntoLight = canComboIntoLight;
    }
}
