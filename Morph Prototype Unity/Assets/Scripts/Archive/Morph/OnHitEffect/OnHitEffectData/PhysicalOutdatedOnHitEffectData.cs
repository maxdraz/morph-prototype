using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Physical", menuName = "Weapon Morph/On Hit Effect Data/Physical")]
public class PhysicalOutdatedOnHitEffectData : OutdatedOnHitEffectData
{
    [SerializeField] private float damage;

    public float Damage => damage;

    public override OutdatedOnHitEffect CreateOnHitEffectInstance(OutdatedMorph owner, OutdatedDamageHandler ownerOutdatedDamageHandler)
    {
        return new PhysicalOutdatedOnHitEffect(this);
    }
}
