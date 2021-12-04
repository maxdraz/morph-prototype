using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AcidDamageData : OnHitEffectData, IAcidDamage
{
    [SerializeField] private float acidDamage;

    public AcidDamageData(float acidDamage = 1)
    {
        this.acidDamage = acidDamage;
    }

    public float AcidDamage
    {
        get => acidDamage;
        set => acidDamage = value;
    }
    
    public override object Clone()
    {
        return new AcidDamageData(acidDamage);
    }
}

[CreateAssetMenu(fileName = "Acid Damage", menuName = "On Hit Effects/Acid Damage")]
public class AcidDamageOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new AcidDamageData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is AcidDamageData acidDamageData)
        {
            damageTaker.ApplyDamage(acidDamageData, damageDealer);
        }
    }
}