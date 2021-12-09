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
                0);
            
            damageTaker.ApplyDamage(data, damageDealer);
        }
        
    }
}
