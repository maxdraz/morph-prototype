using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrutalSpursMorph : WeaponMorph
{
    protected override void Awake()
    {
        base.Awake();
        
        lightAttacks = new List<LightAttack>()
        {
            new SpurStab(0.5f, 0.2f),
            new SpurStab(0.5f, 0.2f),
            new SpurStab(0.5f,0)
        };

        heavyAttacks = new List<HeavyAttack>()
        {
            new SpurHeavySlash(2),
            new SpurHeavySlash(2),
            new SpurHeavySlash(3)
        };
    }
}
