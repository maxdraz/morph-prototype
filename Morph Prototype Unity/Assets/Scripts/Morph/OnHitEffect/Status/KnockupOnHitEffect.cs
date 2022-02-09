using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KnockupData : OnHitEffectData, IKnockUp
{
    [SerializeField] private float knockupForce;
    public KnockupData(float knockupForce = 100)
    {
        this.knockupForce = knockupForce;
    }

    public override object Clone()
    {
        return new KnockupData(knockupForce);
    }

    public float KnockupForce
    {
        get => knockupForce;
        set => knockupForce = value;
    }
}

[CreateAssetMenu(fileName = "Knockup", menuName = "On Hit Effects/Knockup")]
public class KnockupOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new KnockupData(100);
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is KnockupData knockupData)
        {
            damageTaker.ApplyDamage(data, damageDealer);
        }
    }
}
