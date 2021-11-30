using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoisonOnHitEffect : OnHitEffect
{
    //TODO - make below var private! 
    [SerializeField] public float baseDuration;
    [SerializeField] public float baseTickRate;
    [SerializeField] public float actualDuration;
    [SerializeField] public float actualTickRate;
    private PoisonOnHitEffectData data;
    

    public PoisonOnHitEffect(float baseDuration = 1f, float baseTickRate = 0.5f)
    {
        this.baseDuration = baseDuration;
        this.baseTickRate = baseTickRate;

        actualDuration = this.baseDuration;
        actualTickRate = this.baseTickRate;
    }
    
    public PoisonOnHitEffect(PoisonOnHitEffectData data)
    {
        this.data = data;
        baseDuration = data.duration;
        baseTickRate = data.tickRate;

        actualDuration = this.baseDuration;
        actualTickRate = this.baseTickRate;
    }

    public override void Reset()
    {
        actualDuration = baseDuration;
        actualTickRate = baseTickRate;
    }

    public override void Apply(DamageHandler damageTaker)
    {
        var poisonDOT = new PoisonDamageOverTime(5, actualDuration, actualTickRate, data);
        damageTaker.ApplyDebuff(poisonDOT);
    }
}
