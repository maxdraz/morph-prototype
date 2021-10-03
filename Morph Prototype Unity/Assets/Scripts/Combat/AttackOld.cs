using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class AttackOld
{
    protected float baseDamage;
    protected float fortitudeDamage;
    protected float staminaCost;
    protected float energyCost;
    protected float critChance;
    protected float attackSpeed;
    protected float duration;
    protected float nextComboInputWindow;

    public bool completed;

    public AttackOld(float baseDamage, float fortitudeDamage, float staminaCost, float energyCost, float critChance, float attackSpeed, float duration, float nextComboInputWindow)
    { 
        this.baseDamage = baseDamage;
        this.fortitudeDamage = fortitudeDamage;
        this.staminaCost = staminaCost;
        this.energyCost = energyCost;
        this.critChance = critChance;
        this.attackSpeed= attackSpeed;
        this.duration = duration;
        this.nextComboInputWindow = nextComboInputWindow;
        completed = false;
    }
}
