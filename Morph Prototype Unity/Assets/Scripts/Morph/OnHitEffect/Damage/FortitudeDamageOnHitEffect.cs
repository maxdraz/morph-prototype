using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FortitudeDamageData : OnHitEffectData, IFortitudeDamage
{
    [SerializeField] private float fortitudeDamage;
    [SerializeField] private string statusEffect;

    public FortitudeDamageData(float fortitudeDamage = 1, string statusEffect = "")
    {
        this.fortitudeDamage = fortitudeDamage;
        this.statusEffect = statusEffect;
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
    
    public override object Clone()
    {
        return new FortitudeDamageData(fortitudeDamage, statusEffect);
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