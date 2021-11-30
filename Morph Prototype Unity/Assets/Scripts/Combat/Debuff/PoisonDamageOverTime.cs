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
    private Morph owner;
    private DamageHandler ownerDamageHandler;

    public PoisonDamageOverTime(float duration, float tickInterval, PoisonOnHitEffectData data, Morph owner, 
        DamageHandler ownerDamageHandler)
    {
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.data = data;
        poisonDamage = data.damage;
        this.owner = owner;
        this.ownerDamageHandler = ownerDamageHandler;
        
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
                ApplyDebuff(damageTaker);
            }
        }

        if (durationTimer.JustFinished)
        {
            ApplyDebuff(damageTaker);
        }
    }

    public override bool IsFinished()
    {
        return durationTimer.IsFinished();
    }

    public override void ApplyDebuff(DamageHandler damageTaker)
    {
        Debug.Log("basePoison damage = " + poisonDamage);
        var actualPoisonDamage =
            DamageCalculator.ElementalDamage(poisonDamage, ownerDamageHandler.Stats.ChemicalDamageModifier, 0, 0);
        Debug.Log("actualPoison damage = " + actualPoisonDamage);
        damageTaker.ApplyDamage(actualPoisonDamage, DamageType.Poison);
        
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
