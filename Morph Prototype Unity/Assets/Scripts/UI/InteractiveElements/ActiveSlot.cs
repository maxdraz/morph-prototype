using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSlot : Slot
{
    private ActiveMorph activeMorph; // reference morph in slot
  
    
    protected override void Awake()
    {
        base.Awake();
        dropConditions.Add(new IsLimbWeaponMorphDropCondition());
    }
    
    // cooldown
    private void DisplayCooldown()
    {
        
    }
    
}
