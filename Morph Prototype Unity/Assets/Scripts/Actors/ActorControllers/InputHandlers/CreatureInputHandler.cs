using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureInputHandler : MonoBehaviour
{
    public abstract bool GetAppendageLightAttackInput();
    public abstract bool GetAppendageHeavyAttackInput();
    public abstract bool GetMouthLightAttackInput();
    public abstract bool GetMouthHeavyAttackInput();
    public abstract bool GetTailLightAttackInput();
    public abstract bool GetTailHeavyAttackInput();
    
}
