using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightningDamageData : OnHitEffectData, ILightningDamage
{
    [SerializeField] private float lightningDamage;

    public LightningDamageData(float lightningDamage = 1)
    {
        this.lightningDamage = lightningDamage;
    }

    public float LightningDamage
    {
        get => lightningDamage;
        set => lightningDamage = value;
    }
    
    public override object Clone()
    {
        return new LightningDamageData(lightningDamage);
    }
}

[CreateAssetMenu(fileName = "Lightning Damage", menuName = "On Hit Effects/Lightning Damage")]
public class LightningDamageOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new LightningDamageData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is LightningDamageData lightningDamageData)
        {
            damageTaker.ApplyDamage(lightningDamageData, damageDealer);
        }
    }
}