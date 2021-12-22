using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadWeaponMorphSlot : Slot
{
    protected override void Awake()
    {
        base.Awake();
        dropConditions.Add(new IsHeadWeaponMorphDropCondition());
    }
}
