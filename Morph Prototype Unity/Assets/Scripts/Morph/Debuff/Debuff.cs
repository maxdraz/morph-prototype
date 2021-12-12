using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffContributor
{
    public DamageHandler damageDealer;
    public float damageContributed;

    public DebuffContributor(DamageHandler damageDealer, float damageContributed)
    {
        this.damageDealer = damageDealer;
        this.damageContributed = damageContributed;
    }
}

[System.Serializable]
public abstract class Debuff
{
    // timer tick timer
    [SerializeField] protected LegacyTimer tickLegacyTimer;
    [SerializeField] protected float damageStack;
    // TODO - support multiple contributors
    protected IDamageType damageType;
    [SerializeField] protected DamageHandler damageDealer;

    public LegacyTimer TickLegacyTimer => tickLegacyTimer;
    public DamageHandler DamageDealer => damageDealer;

    public Debuff(LegacyTimer tickLegacyTimer)
    {
        this.tickLegacyTimer = tickLegacyTimer;
    }

    public virtual void OnStart(float debuffDuration)
    {
        
    }

    public virtual bool CountdownTimer(float dt)
    { 
        return tickLegacyTimer.CountDown(dt);
    }

    public virtual bool ShouldTick()
    {
      
        return tickLegacyTimer.JustFinished;
    }

    public void ChangeTickInterval(float newInterval)
    {
        tickLegacyTimer.Duration = newInterval;
    }

    public void UpdateTickInterval(float newInterval)
    {
        tickLegacyTimer.Duration = newInterval;
    }

    public abstract IDamageType GetTickDamage();
   

    public virtual bool IsFinished()
    {
        return damageStack <= 0;
    }

    public void AddDebuffContributor(DamageHandler dmgDealer, float damageContribution, float debuffDuration = 0)
    {
       // debuffContributors.Add(new DebuffContributor(damageDealer, damageContribution));
       this.damageDealer = dmgDealer;
       damageStack += damageContribution;
       RestartDebuffTimer(debuffDuration);
    }

    public virtual void RestartDebuffTimer(float debuffDuration)
    {
       
    }


}
