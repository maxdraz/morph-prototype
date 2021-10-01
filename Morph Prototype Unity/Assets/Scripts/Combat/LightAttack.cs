using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : Attack
{
    public bool CanComboIntoHeavy { get; }

    public LightAttack(float baseDamage, float fortitudeDamage, float staminaCost, float energyCost, float critChance, float attackSpeed, float duration, float nextComboInputWindow, bool canComboIntoHeavy) 
        : base(baseDamage, fortitudeDamage, staminaCost, energyCost, critChance, attackSpeed, duration, nextComboInputWindow)
    {
        this.CanComboIntoHeavy = canComboIntoHeavy;
    }
}
