using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpurHeavySlash : HeavyAttack
{
    public SpurHeavySlash(float duration, float inputNextWindow = 0.5f, bool canComboIntoOtherType = true) 
        : base(duration, inputNextWindow, canComboIntoOtherType)
    {
        name = "Spur Heavy Slash";
    }
}
