using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Critical Strike Chance", menuName = "On Hit Effects/Normal Physical Damage")]
public class CriticalStrike : OnHitEffect
{
    [SerializeField] float criticalStrikeChance;


    public override OnHitEffectData GetData()
    {
        return new PhysicalDamageData();
    }

    

    private void OnDamageAboutToBeDealt(ref IDamageType damageType)
    {
        if (Random.Range(0,100) <= criticalStrikeChance) 
        {
            if (damageType is IPhysicalDamage physicalDamage)
            {

            }
        }
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is IPhysicalDamage physicalDamage)
        {
            damageTaker.ApplyDamage(data, damageDealer);
        }
    }
}
