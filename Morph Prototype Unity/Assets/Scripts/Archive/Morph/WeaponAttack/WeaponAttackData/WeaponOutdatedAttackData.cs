using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponOutdatedAttackData : OutdatedAttackData
{
    [Header("Hitbox")]
    [SerializeField] private HitboxType hitBoxType;

    [Header("On Hit Effects")]
    [SerializeField] protected List<OutdatedOnHitEffectData> onHitEffects;

    public HitboxType HitBoxType => hitBoxType;

    public abstract OutdatedWeaponAttack CreateWeaponAttackInstance(GameObject owner, OutdatedMorph ownerOutdatedMorph, OutdatedDamageHandler ownerOutdatedDamageHandler);

    public virtual List<OutdatedOnHitEffect> CreateOnHitEffectInstances(OutdatedMorph ownerOutdatedMorph, OutdatedDamageHandler ownerOutdatedDamageHandler)
    {
        List<OutdatedOnHitEffect> onHitEffectInstances = new List<OutdatedOnHitEffect>();
        if (onHitEffects.Count > 0)
        {
            foreach (var onHitEffect in onHitEffects)
            {
                onHitEffectInstances.Add(onHitEffect.CreateOnHitEffectInstance(ownerOutdatedMorph,ownerOutdatedDamageHandler));
            }
        }
        return onHitEffectInstances;
    }

}
