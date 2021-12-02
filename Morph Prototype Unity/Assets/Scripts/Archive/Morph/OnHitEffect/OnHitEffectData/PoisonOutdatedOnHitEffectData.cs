using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "Weapon Morph/On Hit Effect Data/Poison")]
public class PoisonOutdatedOnHitEffectData : OutdatedOnHitEffectData
{
    [Header("Poison")]
    public float damage;
    [SerializeField] public float duration;
    [SerializeField] public float tickRate;


    public override OutdatedOnHitEffect CreateOnHitEffectInstance(OutdatedMorph owner, OutdatedDamageHandler ownerOutdatedDamageHandler)
    {
        var poisonOnHitEffect = new PoisonOutdatedOnHitEffect(this, owner, ownerOutdatedDamageHandler);
        return poisonOnHitEffect;
    }
}
