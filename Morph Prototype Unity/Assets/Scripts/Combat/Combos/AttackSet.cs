using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackSet
{
    public List<LightAttack> lightAttacks;
    public List<HeavyAttack> heavyAttacks;

    public AttackSet(List<LightAttack> lightAttacks, List<HeavyAttack> heavyAttacks)
    {
        this.lightAttacks = lightAttacks;
        this.heavyAttacks = heavyAttacks;
    }
}
