using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Piercing Physical Damage", menuName = "On Hit Effects/Piercing Physical Damage")]
public class PiercingDamageOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new PiercingDamageData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is IPiercingDamage piercingDamage)
        {
            piercingDamage.PhysicalDamageDealt = DamageFormulas.PhysicalDamage(
                piercingDamage.MorphDamage,
                damageDealer.Stats.MeleeDamageModifier,
                piercingDamage.StrikeModifier,
                0,
                0,
                damageDealer.Stats.globalCritChance,
                 0, // this is weaponCritChance. Find the crit chance for this strike in the weapons combo (can be 0)
                0); // this is attackCritChance. Find the crit chance for this attack (can be 0)

            damageTaker.ApplyDamage(data, damageDealer);
        }
        
    }
}
