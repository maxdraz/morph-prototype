using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdatedPoisonDamageOverTime : OutdatedDebuff
{
    private float duration;
    private float tickInterval;
    // poisonDamageData ?? 
    private float poisonDamage;
    private Timer durationTimer;
    private Timer tickTimer;
    private PoisonOutdatedOnHitEffectData data;
    private OutdatedMorph owner;
    private OutdatedDamageHandler _ownerOutdatedDamageHandler;

    public OutdatedPoisonDamageOverTime(float duration, float tickInterval, PoisonOutdatedOnHitEffectData data, OutdatedMorph owner, 
        OutdatedDamageHandler ownerOutdatedDamageHandler)
    {
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.data = data;
        poisonDamage = data.damage;
        this.owner = owner;
        this._ownerOutdatedDamageHandler = ownerOutdatedDamageHandler;
        
        durationTimer = new Timer(duration);
        tickTimer = new Timer(tickInterval, true);
    }

    public override void OnUpdate(OutdatedDamageHandler outdatedDamageTaker, float dt)
    {
        if (durationTimer.CountDown(dt))
        {
            tickTimer.CountDown(dt);
            if (tickTimer.JustFinished)
            {
                ApplyDebuff(outdatedDamageTaker);
            }
        }

        if (durationTimer.JustFinished)
        {
            ApplyDebuff(outdatedDamageTaker);
        }
    }

    public override bool IsFinished()
    {
        return durationTimer.IsFinished();
    }

    public override void ApplyDebuff(OutdatedDamageHandler outdatedDamageTaker)
    {
        Debug.Log("basePoison damage = " + poisonDamage);
        var actualPoisonDamage =
            DamageCalculator.ElementalDamage(poisonDamage, _ownerOutdatedDamageHandler.Stats.ChemicalDamageModifier, 0, 0);
        Debug.Log("actualPoison damage = " + actualPoisonDamage);
        outdatedDamageTaker.ApplyDamage(actualPoisonDamage, OutdatedDamageType.Poison);
        
        if (data.onHitParticles)
        {
            var damageTakerTransform = outdatedDamageTaker.transform;
            GameplayStatics.SpawnParticleSystem(
                data.onHitParticles,
                damageTakerTransform,
                damageTakerTransform.position,
                damageTakerTransform.rotation);
        }
    }
}
