using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWeaponMorphSlot : Slot
{
    protected override void Awake()
    {
        base.Awake();
        
        dropConditions.Add(new IsTailWeaponMorphDropCondition());
    }
}
