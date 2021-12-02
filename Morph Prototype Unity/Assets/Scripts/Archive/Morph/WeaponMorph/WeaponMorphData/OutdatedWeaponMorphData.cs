using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OutdatedWeaponMorphData : ScriptableObject
{
    [SerializeField] private float baseDamage = 10;
    // TODO - rarity generation settings
    // TODO - mask field for creature types

    [SerializeField] private List<LightWeaponOutdatedAttackData> lightAttacks;
    [SerializeField] private List<HeavyWeaponOutdatedAttackData> heavyAttacks;

    public abstract WeaponOutdatedMorph CreateWeaponMorphInstance(GameObject owner, OutdatedDamageHandler ownerOutdatedDamageHandler, OutdatedWeaponMorphData data);

    public List<LightOutdatedWeaponAttack> CreateLightWeaponAttackInstances(GameObject owner, OutdatedMorph ownerOutdatedMorph, OutdatedDamageHandler ownerOutdatedDamageHandler)
    {
        var lightAttackInstances = new List<LightOutdatedWeaponAttack>();
        if (lightAttacks.Count < 1) return lightAttackInstances;

        foreach (var lightAttackData in lightAttacks)
        {
            lightAttackInstances.Add((LightOutdatedWeaponAttack)lightAttackData.CreateWeaponAttackInstance(owner, ownerOutdatedMorph, ownerOutdatedDamageHandler));
        }

        return lightAttackInstances;
    }
    
    public List<HeavyOutdatedWeaponAttack> CreateHeavyWeaponAttackInstances(GameObject owner, OutdatedMorph ownerOutdatedMorph, OutdatedDamageHandler ownerOutdatedDamageHandler)
    {
        var heavyAttackInstances = new List<HeavyOutdatedWeaponAttack>();
        if (heavyAttacks.Count < 1) return heavyAttackInstances;

        foreach (var heavyAttackData in heavyAttacks)
        {
            if(heavyAttackData != null)
                heavyAttackInstances.Add((HeavyOutdatedWeaponAttack)heavyAttackData.CreateWeaponAttackInstance(owner, ownerOutdatedMorph, ownerOutdatedDamageHandler));
        }

        return heavyAttackInstances;
    }

}
