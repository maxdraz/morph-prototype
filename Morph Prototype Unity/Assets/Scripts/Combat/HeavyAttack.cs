using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeavyAttack : Attack
{
    public HeavyAttack(float duration, float inputNextWindow = 0.5f, bool canComboIntoOtherType = true) 
        : base(duration, inputNextWindow, canComboIntoOtherType)
    {
        isLightAttack = false;
    }
}
