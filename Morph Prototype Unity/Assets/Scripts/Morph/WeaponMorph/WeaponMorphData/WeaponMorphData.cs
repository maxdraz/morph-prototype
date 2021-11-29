using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponMorphData : ScriptableObject
{
    [SerializeField] private float baseDamage = 10;
    // TODO - rarity generation settings
    // TODO - mask field for creature types

    [SerializeField] private List<LightWeaponAttackData> lightAttacks;
    [SerializeField] private List<HeavyWeaponAttackData> heavyAttacks;

    public abstract WeaponMorph CreateWeaponMorphInstance(GameObject owner);

    public List<LightWeaponAttack> CreateLightWeaponAttackInstances(GameObject owner)
    {
        var lightAttackInstances = new List<LightWeaponAttack>();
        if (lightAttacks.Count < 1) return lightAttackInstances;

        foreach (var lightAttackData in lightAttacks)
        {
            lightAttackInstances.Add((LightWeaponAttack)lightAttackData.CreateWeaponAttackInstance(owner));
        }

        return lightAttackInstances;
    }
    
    public List<HeavyWeaponAttack> CreateHeavyWeaponAttackInstances(GameObject owner)
    {
        var heavyAttackInstances = new List<HeavyWeaponAttack>();
        if (heavyAttacks.Count < 1) return heavyAttackInstances;

        foreach (var heavyAttackData in heavyAttacks)
        {
            if(heavyAttackData != null)
                heavyAttackInstances.Add((HeavyWeaponAttack)heavyAttackData.CreateWeaponAttackInstance(owner));
        }

        return heavyAttackInstances;
    }

}
