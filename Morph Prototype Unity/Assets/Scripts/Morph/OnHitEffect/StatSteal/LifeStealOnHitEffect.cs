using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LifeStealData : OnHitEffectData, ILifestealDamage
{
    [SerializeField] private LifestealFXData lifestealFXData;
    [SerializeField] private float lifestealAmount;

    public LifeStealData(float lifestealAmount = 1, LifestealFXData lifestealFXData = null)
    {
        this.lifestealAmount = lifestealAmount;
        this.lifestealFXData = lifestealFXData;
    }
    
    public override object Clone()
    {
        return new LifeStealData(lifestealAmount, lifestealFXData);
    }

    public float LifestealAmount
    {
        get => lifestealAmount;
        set => lifestealAmount = value;
    }
    
    public LifestealFXData LifestealFXData
    {
        get => lifestealFXData;
    }
}

[CreateAssetMenu(fileName = "Lifesteal", menuName = "On Hit Effects/Lifesteal")]
public class LifeStealOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new LifeStealData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is LifeStealData)
        {
            damageTaker.ApplyDamage(data, damageDealer);
        }
    }
}
