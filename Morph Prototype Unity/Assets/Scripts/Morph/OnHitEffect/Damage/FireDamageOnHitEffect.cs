using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireDamageData : OnHitEffectData, IFireDamage
{
    [SerializeField] private float fireDamage;

    public FireDamageData(float fireDamage = 1)
    {
        this.fireDamage = fireDamage;
    }

    public float FireDamage
    {
        get => fireDamage;
        set => fireDamage = value;
    }
    
    public override object Clone()
    {
        return new FireDamageData(fireDamage);
    }
}

[CreateAssetMenu(fileName = "Fire Damage", menuName = "On Hit Effects/Fire Damage")]
public class FireDamageOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new FireDamageData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is FireDamageData fireDamageData)
        {
            damageTaker.ApplyDamage(fireDamageData, damageDealer);
        }
    }
}
