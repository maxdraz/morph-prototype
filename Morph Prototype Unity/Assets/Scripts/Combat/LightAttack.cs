using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightAttack : Attack
{
    public LightAttack(float duration, float inputNextWindow = 0.5f, bool canComboIntoOtherType = true) 
        : base(duration, inputNextWindow, canComboIntoOtherType)
    {
        isLightAttack = true;
    }
}
