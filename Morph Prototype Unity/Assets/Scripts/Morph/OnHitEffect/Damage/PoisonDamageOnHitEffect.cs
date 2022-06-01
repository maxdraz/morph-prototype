using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoisonDamageData : OnHitEffectData, IPoisonDamage
{
    [SerializeField] private float poisonDamage;
    [SerializeField] private AttackType attackType;
    public AttackType AttackType { get => attackType;
        set => attackType = value;
    }

    public PoisonDamageData(float poisonDamage = 1, AttackType attackType = AttackType.Melee)
    {
        this.poisonDamage = poisonDamage;
        this.attackType = attackType;
    }
    
    public override object Clone()
    {
        return new PoisonDamageData(poisonDamage, attackType);
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
            damageTaker.ApplyDebuff(data,damageDealer);
        }
    }
}
