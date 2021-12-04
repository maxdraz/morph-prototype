using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoisonDamageData : OnHitEffectData, IPoisonDamage
{
    [SerializeField] private float poisonDamage;

    public PoisonDamageData(float poisonDamage = 1)
    {
        this.poisonDamage = poisonDamage;
    }
    
    public override object Clone()
    {
        return new PoisonDamageData(poisonDamage);
    }

    public float PoisonDamage
    {
        get => poisonDamage;
        set => poisonDamage = value;
    }
}


[CreateAssetMenu(fileName = "Poison Damage", menuName = "On Hit Effects/Poison Damage")]
public class PoisonDamageOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new PoisonDamageData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is PoisonDamageData poisonDamageData)
        {
            // calculate poison damage ...
            damageTaker.ApplyDamage(data,damageDealer);
        }
    }
}
