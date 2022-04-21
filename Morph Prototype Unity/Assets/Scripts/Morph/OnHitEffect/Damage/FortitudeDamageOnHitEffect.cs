using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FortitudeDamageData : OnHitEffectData, IFortitudeDamage
{
    [SerializeField] private float fortitudeDamage;
    [SerializeField] private string statusEffect;
    [SerializeField] private float duration;
   // public enum statusEffect 
   // {
   // Stun, 
   // Root, 
   // Silence, 
   // Blindness, 
   // Paralysis, 
   // Crippled
   // }

    public FortitudeDamageData(float fortitudeDamage = 1, string statusEffect = "", float duration = 0)
    {
        this.fortitudeDamage = fortitudeDamage;
        this.statusEffect = statusEffect;
        this.duration = duration;
    }

    public float FortitudeDamage
    {
        get => fortitudeDamage;
        set => fortitudeDamage = value;
    }

    public string StatusEffect 
    {
        get => statusEffect;
        set => statusEffect = value;
    }

    public float Duration
    {
        get => duration;
        set => duration = value;
    }

    public override object Clone()
    {
        return new FortitudeDamageData(fortitudeDamage, statusEffect, duration);
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