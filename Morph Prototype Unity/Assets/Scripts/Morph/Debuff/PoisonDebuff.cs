using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : Debuff
{
    public PoisonDebuff(Timer tickTimer) : base(tickTimer)
    {
    }

    public override IDamageType GetTickDamage()
    {
        float tickDamage = (50f + (damageStack / 20f)) * 1.5f;
        damageStack -= tickDamage;
        return new PoisonDamageData(tickDamage);
    }
}
