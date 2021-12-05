using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AcidDamageData : OnHitEffectData, IAcidDamage
{
    [SerializeField] private float acidDamage;
    private float acidDOTDuration;
    private float acidPercentageBonusDamage = 1;
    private float acidDOTModifier = 1;

    public AcidDamageData(float acidDamage = 1, float acidDOTDuration = 5f, float acidPercentageBonusDamage = 1f, float acidDotModifier = 1f)
    {
        this.acidDamage = acidDamage;
        this.acidDOTDuration = acidDOTDuration;
        this.acidPercentageBonusDamage = acidPercentageBonusDamage;
        this.acidDOTModifier = acidDotModifier;
    }

    public float AcidDamage
    {
        get => acidDamage;
        set => acidDamage = value;
    }

    public float AcidDOTDuration
    {
        get => acidDOTDuration;
        set=> acidDOTDuration = value;
    }

    public float AcidPercentageBonusDamage
    {
        get => acidPercentageBonusDamage; 
        set => acidPercentageBonusDamage = value;
    }

    public float AcidDOTModifier
    {
        get => acidDOTModifier;
        set => acidDOTModifier = value;
    }

    public override object Clone()
    {
        return new AcidDamageData(acidDamage, acidDOTDuration);
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
            acidDamageData.AcidDOTDuration = 5;
            damageTaker.ApplyDebuff(acidDamageData, damageDealer);
        }
    }
}