using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureInputHandler : MonoBehaviour
{
    public abstract bool GetLightAttackInput();
    public abstract bool GetHeavyAttackInput();
}
