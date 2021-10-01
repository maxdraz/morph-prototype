using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericLightAttack : LightAttack
{
    public GenericLightAttack(float baseDamage, float fortitudeDamage, float staminaCost, float energyCost, float critChance, float attackSpeed,float duration, float nextComboInputWindow, bool canComboIntoHeavy) 
        : base(baseDamage, fortitudeDamage, staminaCost, energyCost, critChance, attackSpeed, duration, nextComboInputWindow,canComboIntoHeavy)
    {
    }
}
