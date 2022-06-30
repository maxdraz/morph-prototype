using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect 
{
    Stun, 
    Root, 
    Silence, 
    Blindness, 
    Paralysis, 
    Crippled
}

[System.Serializable]
public class FortitudeDamageData : OnHitEffectData, IFortitudeDamage
{
    [SerializeField] private float fortitudeDamage;
    [SerializeField] private float duration;
    [SerializeField] private AttackType attackType;
    public AttackType AttackType { get => attackType;
        set => attackType = value;
    }
   

   private StatusEffect statusEffect;
   public StatusEffect StatusEffectType => statusEffect;

    public FortitudeDamageData(float fortitudeDamage = 1, StatusEffect statusEffect = StatusEffect.Stun, float duration = 0, AttackType attackType = AttackType.Melee)
    {
        this.fortitudeDamage = fortitudeDamage;
        this.duration = duration;
        this.attackType = attackType;
        this.statusEffect = statusEffect;
    }

    public float FortitudeDamage
    {
        get => fortitudeDamage;
        set => fortitudeDamage = value;
    }

    public float Duration
    {
        get => duration;
        set => duration = value;
    }

    public override object Clone()
    {
        return new FortitudeDamageData(fortitudeDamage, statusEffect, duration, attackType);
    }
}

[CreateAssetMenu(fileName = "Fortitude Damage", menuName = "On Hit Effects/Fortitude Damage")]
public class FortitudeDamageOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new FortitudeDamageData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is FortitudeDamageData fortitudeDamageData)
        {
            damageTaker.ApplyDamage(fortitudeDamageData, damageDealer);
        }
    }
}