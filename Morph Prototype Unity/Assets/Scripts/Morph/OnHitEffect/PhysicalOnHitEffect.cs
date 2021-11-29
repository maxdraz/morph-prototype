using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhysicalOnHitEffect : OnHitEffect
{
    [SerializeField] private float damage;
    [SerializeField] private float actualDamage;

    public PhysicalOnHitEffect(PhysicalOnHitEffectData data)
    {
        damage = data.Damage;
        actualDamage = damage;
    }
    
    public override void Reset()
    {
        actualDamage = damage;
    }
}
