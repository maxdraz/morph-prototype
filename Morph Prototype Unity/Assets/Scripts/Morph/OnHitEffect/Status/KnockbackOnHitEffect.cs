using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KnockbackData : OnHitEffectData, IKnockback
{
    [SerializeField] private float knockbackForce;
    public KnockbackData(float knockbackForce = 100)
    {
        this.knockbackForce = knockbackForce;
    }
    
    public override object Clone()
    {
        return new KnockbackData(knockbackForce);
    }

    public float KnockbackForce
    {
        get => knockbackForce;
        set=> knockbackForce = value;
    }
}

[CreateAssetMenu(fileName = "Knockback", menuName = "On Hit Effects/Knockback")]
public class KnockbackOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new KnockbackData(100);
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is KnockbackData knockbackData)
        {
            damageTaker.ApplyDamage(data, damageDealer);
        }
    }
}
