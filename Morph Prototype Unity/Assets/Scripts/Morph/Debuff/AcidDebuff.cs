using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AcidDebuff : Debuff
{
    [SerializeField] private Timer acidDurationTimer;
    [SerializeField] private float acidDotModifier;


    public AcidDebuff(Timer acidDurationTimer, Timer tickTimer) : base(tickTimer)
    {
        this.acidDurationTimer = acidDurationTimer;
    }

    public override bool IsFinished()
    {
        return acidDurationTimer.IsFinished() || damageStack <= 0;
    }

    public override void OnStart(float debuffDuration)
    {
        tickTimer.Restart();
        RestartDebuffTimer(debuffDuration);
    }

    public override bool CountdownTimer(float dt)
    {
        Debug.Log("should be updating actual duration");
        acidDurationTimer.CountDown(dt);

        if (!acidDurationTimer.JustFinished)
        {
            tickTimer.CountDown(dt);
        }

        return acidDurationTimer.JustFinished;
    }

    public override void RestartDebuffTimer(float debuffDuration)
    {
        
        acidDurationTimer.Restart(debuffDuration);
    }

    public override IDamageType GetTickDamage()
    {
        float acidDamageDealt = 1 + (damageStack / 5);
        damageStack -= acidDamageDealt;
        acidDamageDealt *= (1 + acidDotModifier);
        return new AcidDamageData(acidDamageDealt);
    }
}
