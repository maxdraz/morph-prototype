using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoisonDebuff : Debuff
{
    public PoisonDebuff(LegacyTimer tickLegacyTimer) : base(tickLegacyTimer)
    {
    }

    public override IDamageType GetTickDamage()
    {
        float tickDamage = (50f + (damageStack / 20f)) * 1.5f;
        damageStack -= tickDamage;
        return new PoisonDamageData(tickDamage);
    }
}
