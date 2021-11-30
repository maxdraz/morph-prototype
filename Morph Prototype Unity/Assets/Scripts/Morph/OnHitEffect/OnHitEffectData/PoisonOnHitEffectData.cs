using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "Weapon Morph/On Hit Effect Data/Poison")]
public class PoisonOnHitEffectData : OnHitEffectData
{
    [Header("Poison")]
    [SerializeField] public float duration;
    [SerializeField] public float tickRate;

    public override OnHitEffect CreateOnHitEffectInstance()
    {
        var poisonOnHitEffect = new PoisonOnHitEffect(this);
        return poisonOnHitEffect;
    }
}
