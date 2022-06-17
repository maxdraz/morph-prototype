using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rugged : PassiveMorph
{
    [SerializeField] private int toughnessStatBonus = 5;

    [SerializeField] private bool unlockUnbreakable = true;
    [SerializeField] private float flatPhysicalDamageReduction;

    private Stats stats;
    
    protected override void OnEquip()
    {
        base.OnEquip();
        
        ChangeToughnessStat(toughnessStatBonus);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ChangeToughnessStat(-toughnessStatBonus);
    }

    // implement
    private void ChangeToughnessStat(int amountToAdd)
    {
        stats.FlatStatChange("toughness", amountToAdd);
    }

    private void OnDamageAboutToBeTaken(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PhysicalDamage > 0 && unlockUnbreakable)
        {
            damageTakenSummary.PhysicalDamage -= flatPhysicalDamageReduction;
        }
    }
}
