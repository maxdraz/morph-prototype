using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedingData : OnHitEffectData, IBleeding
{
    [SerializeField] private int bleedValue;
    public BleedingData(int bleedValue = 1)
    {
        this.bleedValue = bleedValue;
    }

    public override object Clone()
    {
        return new BleedingData(bleedValue);
    }

    public int BleedValue
    {
        get => bleedValue;
        set => bleedValue = value;
    }
}

[CreateAssetMenu(fileName = "Bleeding", menuName = "On Hit Effects/Bleeding")]
public class BleedingOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new BleedingData(1);
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is BleedingData bleedingData)
        {
            damageTaker.ApplyDamage(data, damageDealer);
        }
    }
}