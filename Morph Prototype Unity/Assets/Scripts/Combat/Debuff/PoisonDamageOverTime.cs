using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamageOverTime : Debuff
{
    private float duration;
    private float tickInterval;
    // poisonDamageData ?? 
    private float poisonDamage;
    private Timer durationTimer;
    private Timer tickTimer;
    private PoisonOnHitEffectData data;

    public PoisonDamageOverTime(float poisonDamage, float duration, float tickInterval, PoisonOnHitEffectData data)
    {
        this.poisonDamage = poisonDamage;
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.data = data;
        
        durationTimer = new Timer(duration);
        tickTimer = new Timer(tickInterval, true);
    }

    public override void OnUpdate(DamageHandler damageTaker, float dt)
    {
        if (durationTimer.CountDown(dt))
        {
            tickTimer.CountDown(dt);
            if (tickTimer.JustFinished)
            {
                Debug.Log("poison tick");
                damageTaker.ApplyDamage(poisonDamage);
                if (data.onHitParticles)
                {
                    var damageTakerTransform = damageTaker.transform;
                    GameplayStatics.SpawnParticleSystem(
                        data.onHitParticles,
                        damageTakerTransform,
                        damageTakerTransform.position,
                        damageTakerTransform.rotation);
                }
            }
        }

        if (durationTimer.JustFinished)
        {
            Debug.Log("poison tick");
            damageTaker.ApplyDamage(poisonDamage);
            if (data.onHitParticles)
            {
                var damageTakerTransform = damageTaker.transform;
                GameplayStatics.SpawnParticleSystem(
                    data.onHitParticles,
                    damageTakerTransform,
                    damageTakerTransform.position,
                    damageTakerTransform.rotation);
            }
        }
    }

    public override bool IsFinished()
    {
        return durationTimer.IsFinished();
    }
}
