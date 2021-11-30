using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "Weapon Morph/On Hit Effect Data/Poison")]
public class PoisonOnHitEffectData : OnHitEffectData
{
    [Header("Poison")]
    public float damage;
    [SerializeField] public float duration;
    [SerializeField] public float tickRate;


    public override OnHitEffect CreateOnHitEffectInstance(Morph owner, DamageHandler ownerDamageHandler)
    {
        var poisonOnHitEffect = new PoisonOnHitEffect(this, owner, ownerDamageHandler);
        return poisonOnHitEffect;
    }
}
