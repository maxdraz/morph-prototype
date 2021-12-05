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
    [SerializeField] protected Timer tickTimer;
    [SerializeField] protected float damageStack;
    // TODO - support multiple contributors
    protected IDamageType damageType;
    [SerializeField] protected DamageHandler damageDealer;

    public Timer TickTimer => tickTimer;
    public DamageHandler DamageDealer => damageDealer;

    public Debuff(Timer tickTimer)
    {
        this.tickTimer = tickTimer;
    }

    public virtual void OnStart(float debuffDuration)
    {
        
    }

    public virtual bool CountdownTimer(float dt)
    { 
        return tickTimer.CountDown(dt);
    }

    public virtual bool ShouldTick()
    {
        Debug.Log("tick");
        return tickTimer.JustFinished;
    }

    public void ChangeTickInterval(float newInterval)
    {
        tickTimer.Duration = newInterval;
    }

    public void UpdateTickInterval(float newInterval)
    {
        tickTimer.Duration = newInterval;
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
       Debug.Log("damage stack " + damageStack);
    }

    public virtual void RestartDebuffTimer(float debuffDuration)
    {
       
    }


}
