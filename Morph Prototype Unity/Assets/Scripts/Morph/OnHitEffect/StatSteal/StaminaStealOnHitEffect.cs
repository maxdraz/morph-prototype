using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class StaminaStealData : OnHitEffectData, IStaminaSteal
{
    [SerializeField] private StaminastealFXData staminastealFXData;
    [SerializeField] private float staminastealAmount;

    public StaminaStealData(float staminastealAmount = 1, StaminastealFXData staminastealFXData = null)
    {
        this.staminastealAmount = staminastealAmount;
        this.staminastealFXData = staminastealFXData;
    }

    public override object Clone()
    {
        return new StaminaStealData(staminastealAmount, staminastealFXData);
    }

    public float StaminaToSteal
    {
        get => staminastealAmount;
        set => staminastealAmount = value;
    }

    public StaminastealFXData StaminastealFXData
    {
        get => staminastealFXData;
    }

    [CreateAssetMenu(fileName = "StaminaSteal", menuName = "On Hit Effects/Staminasteal")]
    public class StaminaStealOnHitEffect : OnHitEffect
    {
        public override OnHitEffectData GetData()
        {
            return new StaminaStealData();
        }

        public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
        {
            if (data is StaminaStealData)
            {
                damageTaker.ApplyDamage(data, damageDealer);
            }
        }
    }

    
}
