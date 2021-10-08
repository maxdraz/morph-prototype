using System;
using System.Collections.Generic;
using UnityEngine;

public class TestWeaponMorph : WeaponMorph
{
    protected override void InitializeAttacks()
    {
        lightAttacks = new List<LightAttack>()
        {
            new SpurStab(1),
            new SpurStab(2),
            new SpurStab(3,0,false)
        };

        heavyAttacks = new List<HeavyAttack>()
        {
            new SpurHeavySlash(2),
            new SpurHeavySlash(2),
            new SpurHeavySlash(3,0,false)
        };

        lightAttacks[0].name += " 1";
        lightAttacks[1].name += " 2";
        lightAttacks[2].name += " 3";
        
        heavyAttacks[0].name += " 1";
        heavyAttacks[1].name += " 2";
        heavyAttacks[2].name += " 3";
    }
}
