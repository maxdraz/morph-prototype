using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IceDamageData : OnHitEffectData, IIceDamage
{
    [SerializeField] private float iceDamage;

    public IceDamageData(float iceDamage = 1)
    {
        this.iceDamage = iceDamage;
    }

    public float IceDamage
    {
        get => iceDamage;
        set => iceDamage = value;
    }
    
    public override object Clone()
    {
        return new IceDamageData(iceDamage);
    }
}

[CreateAssetMenu(fileName = "Ice Damage", menuName = "On Hit Effects/Ice Damage")]
public class IceDamageOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new IceDamageData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is IceDamageData iceDamageData)
        {
            damageTaker.ApplyDamage(iceDamageData, damageDealer);
        }
    }
}