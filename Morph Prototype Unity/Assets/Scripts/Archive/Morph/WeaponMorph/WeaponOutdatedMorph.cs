using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class WeaponOutdatedMorph : OutdatedMorph
{
   [SerializeField] private OutdatedWeaponMorphData data;
   [SerializeField] private int currentLightAttackIndex;
   [SerializeField] private int currentHeavyAttackIndex;
   private GameObject owner;
   
   //TODO - make light attacks private! 
   [SerializeField] public List<LightOutdatedWeaponAttack> lightAttacks;
   [SerializeField] private List<HeavyOutdatedWeaponAttack> heavyAttacks;

   public OutdatedWeaponMorphData Data => data;
   
    public WeaponOutdatedMorph(GameObject owner, OutdatedDamageHandler ownerOutdatedDamageHandler, OutdatedWeaponMorphData data)
    {
        this.owner = owner;
        this.data = data;
        lightAttacks = data.CreateLightWeaponAttackInstances(owner, this, ownerOutdatedDamageHandler);
        heavyAttacks = data.CreateHeavyWeaponAttackInstances(owner, this, ownerOutdatedDamageHandler);
    }

    public OutdatedWeaponAttack GetCurrentAttack(WeaponAttackType attackType)
    {
        if (attackType == WeaponAttackType.Light)
        {
            if (lightAttacks.Count < 1) return null;
            return lightAttacks[currentLightAttackIndex];
        }

        if (heavyAttacks.Count < 1) return null;
        return heavyAttacks[currentHeavyAttackIndex];
    }

    public void AdvanceCombo(WeaponAttackType attackType)
    {
        if (attackType == WeaponAttackType.Light)
        {
            currentLightAttackIndex++;
            currentLightAttackIndex %= lightAttacks.Count;
            return;
        }

        currentHeavyAttackIndex++;
        currentHeavyAttackIndex %= heavyAttacks.Count;
    }

    public void ResetCombo()
    {
        currentLightAttackIndex = 0;
        currentHeavyAttackIndex = 0;
    }

    
    
    
}
