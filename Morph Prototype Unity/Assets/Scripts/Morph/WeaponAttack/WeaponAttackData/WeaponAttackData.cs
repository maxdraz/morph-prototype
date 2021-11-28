using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAttackData : AttackData
{
    [Header("Hitbox")]
    [SerializeField] private HitboxType hitBoxType;

    [Header("On Hit Effects")]
    [SerializeField] protected List<OnHitEffectData> onHitEffects;

    public HitboxType HitBoxType => hitBoxType;

    public abstract WeaponAttack CreateWeaponAttackInstance(GameObject owner);

    public List<OnHitEffect> CreateOnHitEffectInstances()
    {
        List<OnHitEffect> onHitEffectInstances = new List<OnHitEffect>();
        if (onHitEffects.Count > 0)
        {
            foreach (var onHitEffect in onHitEffects)
            {
                onHitEffectInstances.Add(onHitEffect.CreateOnHitEffectInstance());
            }
        }
        return onHitEffectInstances;
    }

}
