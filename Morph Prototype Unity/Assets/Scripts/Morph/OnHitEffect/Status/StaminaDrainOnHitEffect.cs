using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaminaDrainData : OnHitEffectData, IStaminaDrain
{
    [SerializeField] private float staminaToDrain = 1f;

    public StaminaDrainData(float staminaToDrain = 1f)
    {
        this.staminaToDrain = staminaToDrain;
    }
    
    public override object Clone()
    {
        return new StaminaDrainData(staminaToDrain);
    }

    public float StaminaToDrain { get => staminaToDrain; set => staminaToDrain = value; }
}

[CreateAssetMenu(fileName = "Stamina Drain", menuName = "On Hit Effects/Stamina Drain")]
public class StaminaDrainOnHitEffect : OnHitEffect
{
    public override OnHitEffectData GetData()
    {
        return new StaminaDrainData();
    }

    public override void ApplyOnHitEffect(OnHitEffectData data, DamageHandler damageTaker, DamageHandler damageDealer)
    {
        if (data is StaminaDrainData staminaDrainData)
        {
            damageTaker.ApplyDamage(data, damageDealer);
        }
    }
}
