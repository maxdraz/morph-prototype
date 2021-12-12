using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AcidDebuff : Debuff
{
    [SerializeField] private LegacyTimer acidDurationLegacyTimer;
    [SerializeField] private float acidDotModifier;
    
    //TODO
    // float damageEachTick = damageStack / 5
    
    // TODO - tick on hit


    public AcidDebuff(LegacyTimer acidDurationLegacyTimer, LegacyTimer tickLegacyTimer) : base(tickLegacyTimer)
    {
        this.acidDurationLegacyTimer = acidDurationLegacyTimer;
    }

    public override bool IsFinished()
    {
        return acidDurationLegacyTimer.IsFinished() || damageStack <= 0;
    }

    public override void OnStart(float debuffDuration)
    {
        tickLegacyTimer.Restart();
        RestartDebuffTimer(debuffDuration);
    }

    public override bool CountdownTimer(float dt)
    {
        acidDurationLegacyTimer.CountDown(dt);

        if (!acidDurationLegacyTimer.JustFinished)
        {
            tickLegacyTimer.CountDown(dt);
        }

        return acidDurationLegacyTimer.JustFinished;
    }

    public override void RestartDebuffTimer(float debuffDuration)
    {
        
        acidDurationLegacyTimer.Restart(debuffDuration);
    }

    public override IDamageType GetTickDamage()
    {
        float acidDamageDealt = 1 + (damageStack / 5);
        damageStack -= acidDamageDealt;
        
        //TODO - acidDamageDealt = damageEachTick;
        return new AcidTickDamageData(acidDamageDealt);
    }
}
