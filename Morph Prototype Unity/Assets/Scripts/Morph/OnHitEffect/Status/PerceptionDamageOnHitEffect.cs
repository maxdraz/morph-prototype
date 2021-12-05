using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerceptionDamageData : OnHitEffectData, IPerceptionDamage
{
    [SerializeField] private float perceptionDamage = 1f;

    public PerceptionDamageData(float perceptionDamage = 1f)
    {
        this.perceptionDamage = perceptionDamage;
    }
    
    public override object Clone()
    {
        return new PerceptionDamageData(perceptionDamage);
    }

    public float PerceptionDamage { get => perceptionDamage; set => perceptionDamage = value; }
}

[CreateAssetMenu(fileName = "Perception Damage", menuName = "On Hit Effects/Perception Damage")]
public class PerceptionDamageOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new PerceptionDamageData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is PerceptionDamageData perceptionDamageData)
        {
            damageTaker.ApplyDamage(data, damageDealer);
        }
    }
}
