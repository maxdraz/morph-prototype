using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Normal Physical Damage")]
public class NormalPhysicalDamageOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new PhysicalDamageData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker)
    {
        if (data is IPhysicalDamage && !data.GetType().IsSubclassOf(typeof(IPhysicalDamage)))
        {
            Debug.Log("physical damage applied effect applied");
            damageTaker.ApplyDamage(data);
        }
    }
}
