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
    

    public PoisonOnHitEffect(float baseDuration = 1f, float baseTickRate = 0.5f)
    {
        this.baseDuration = baseDuration;
        this.baseTickRate = baseTickRate;

        actualDuration = this.baseDuration;
        actualTickRate = this.baseTickRate;
    }

    public override void Reset()
    {
        actualDuration = baseDuration;
        actualTickRate = baseTickRate;
    }
}
