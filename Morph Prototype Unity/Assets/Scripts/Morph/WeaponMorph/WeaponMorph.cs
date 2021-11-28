using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class WeaponMorph
{
   [SerializeField] private WeaponMorphData data;
   [SerializeField] private int currentLightAttackIndex;
   [SerializeField] private int currentHeavyAttackIndex;
   private GameObject owner;
   
   //TODO - make light attacks private! 
   [SerializeField] public List<LightWeaponAttack> lightAttacks;
   [SerializeField] private List<HeavyWeaponAttack> heavyAttacks;

   public WeaponMorphData Data => data;
   
    public WeaponMorph(GameObject owner, WeaponMorphData data)
    {
        this.owner = owner;
        this.data = data;
        lightAttacks = data.CreateLightWeaponAttackInstances(owner);
        heavyAttacks = data.CreateHeavyWeaponAttackInstances(owner);
    }

    public WeaponAttack GetCurrentAttack(WeaponAttackType attackType)
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

    
    
    
}
