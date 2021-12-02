using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoisonOutdatedOnHitEffect : OutdatedOnHitEffect
{
    //TODO - make below var private! 
    [SerializeField] public float baseDuration;
    [SerializeField] public float baseTickRate;
    [SerializeField] public float actualDuration;
    [SerializeField] public float actualTickRate;
    private PoisonOutdatedOnHitEffectData data;
    private OutdatedMorph owner;
    private OutdatedDamageHandler _ownerOutdatedDamageHandler;
    

    public PoisonOutdatedOnHitEffect(float baseDuration = 1f, float baseTickRate = 0.5f)
    {
        this.baseDuration = baseDuration;
        this.baseTickRate = baseTickRate;

        actualDuration = this.baseDuration;
        actualTickRate = this.baseTickRate;
    }
    
    public PoisonOutdatedOnHitEffect(PoisonOutdatedOnHitEffectData data, OutdatedMorph owner, OutdatedDamageHandler ownerOutdatedDamageHandler)
    {
        this.data = data;
        baseDuration = data.duration;
        baseTickRate = data.tickRate;
        this.owner = owner;
        this._ownerOutdatedDamageHandler = ownerOutdatedDamageHandler;

        actualDuration = this.baseDuration;
        actualTickRate = this.baseTickRate;
    }

    public override void Reset()
    {
        actualDuration = baseDuration;
        actualTickRate = baseTickRate;
    }

    public override void Apply(OutdatedDamageHandler outdatedDamageTaker)
    {
        Debug.Log("should apply");
        var poisonDOT = new OutdatedPoisonDamageOverTime(actualDuration, actualTickRate, data, owner, _ownerOutdatedDamageHandler);
        outdatedDamageTaker.ApplyDebuff(poisonDOT);
    }
}
