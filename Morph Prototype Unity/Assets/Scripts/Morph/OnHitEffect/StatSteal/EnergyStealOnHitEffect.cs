using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class EnergyStealData : OnHitEffectData, IEnergySteal
{
    

    [SerializeField] private EnergystealFXData energystealFXData;
    [SerializeField] private float energystealAmount;

    public EnergyStealData(float energyStealAmount = 1, EnergystealFXData energystealFXData = null)
    {
        this.energystealAmount = energystealAmount;
        this.energystealFXData = energystealFXData;
    }

    public override object Clone()
    {
        return new EnergyStealData(energystealAmount, energystealFXData);
    }

    public float EnergystealAmount
    {
        get => energystealAmount;
        set => energystealAmount = value;
    }

    public EnergystealFXData EnergystealFXData
    {
        get => energystealFXData;
    }

    [CreateAssetMenu(fileName = "EnergySteal", menuName = "On Hit Effects/Energysteal")]
    public class EnergyStealOnHitEffect : OnHitEffect
    {
        public override OnHitEffectData GetData()
        {
            return new EnergyStealData();
        }

        public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
        {
            if (data is EnergyStealData)
            {
                damageTaker.ApplyDamage(data, damageDealer);
            }
        }
    }
}
