using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plating : PassiveMorph
{
    [SerializeField] private float bonusMaxArmor = 100;
    [SerializeField] private bool unlockCriticalCoverage;

    [SerializeField] private float critChanceResist = .1f;
    [SerializeField] private float bleedingResist = .2f;
    
    private Armor armor;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        armor = GetComponent<Armor>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ChangeArmorStat(bonusMaxArmor);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ChangeArmorStat(-bonusMaxArmor);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "CriticalCoverage")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockCriticalCoverage = true;
        }
    }

    private void ChangeArmorStat(float amountToAdd)
    {
        //Debug.Log("Plating adding: " + amountToAdd + " to armor stat");
        armor.bonusFlatMaxArmor += amountToAdd;
    }

    private void OnDamageAboutToBeTaken(in DamageTakenSummary damageTakenSummary) 
    {
        
            //damageTakenSummary.critChance -= critChanceResist;
            //damageTakenSummary.bleedingValue -= bleedingResist;
        
    }
}
