using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhysicalOutdatedOnHitEffect : OutdatedOnHitEffect
{
    [SerializeField] private float damage;
    [SerializeField] private float actualDamage;

    public PhysicalOutdatedOnHitEffect(PhysicalOutdatedOnHitEffectData data)
    {
        damage = data.Damage;
        actualDamage = damage;
    }
    
    public override void Reset()
    {
        actualDamage = damage;
    }

    public override void Apply(OutdatedDamageHandler outdatedDamageTaker)
    {
        // implemnt
    }
}
