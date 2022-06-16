using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endurance : PassiveMorph
{
    [SerializeField] private float staminaPercentageStatBonus = .20f;
    [SerializeField] private float staminaPercentageRegenBonus = .40f;
    [SerializeField] private bool unlockMoxie;

    private Stamina stamina;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        stamina = GetComponent<Stamina>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ChangeMaxStaminaStat(staminaPercentageStatBonus);

        if (unlockMoxie)
        {
            stamina.bonusStaminaRegen += staminaPercentageRegenBonus;
        }
    }
    
    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        if (unlockMoxie)
        {
            stamina.bonusStaminaRegen -= staminaPercentageRegenBonus;
        }
    }

    private void ChangeMaxStaminaStat(float amountToAdd)
    {
        BroadcastMessage("SetMaxStamina", amountToAdd);
    }
    
    public void UnlockSecondary(string name)
    {
        if (name == "Moxie")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockMoxie = true;
        }
    }
}
