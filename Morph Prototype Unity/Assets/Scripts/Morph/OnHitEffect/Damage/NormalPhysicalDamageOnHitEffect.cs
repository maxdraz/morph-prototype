using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Normal Physical Damage", menuName = "On Hit Effects/Normal Physical Damage")]
public class NormalPhysicalDamageOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new PhysicalDamageData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is IPhysicalDamage physicalDamage)
        {
            physicalDamage.PhysicalDamageDealt = DamageFormulas.PhysicalDamage(
                physicalDamage.MorphDamage,
                damageDealer.Stats.MeleeDamageModifier,
                physicalDamage.StrikeModifier,
                0,
                0,
                damageDealer.Stats.globalCritChance,
                physicalDamage.WeaponCritChance, // this is weaponCritChance. Find the crit chance for this strike in the weapons combo (can be 0)
                0); // this is attackCritChance. Find the crit chance for this attack (can be 0)

            damageTaker.ApplyDamage(data, damageDealer);
        }
    }
}
