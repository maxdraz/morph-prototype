using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreatureInputHandler : CreatureInputHandler
{
    public override bool GetLightAttackInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    public override bool GetHeavyAttackInput()
    {
        return Input.GetMouseButtonDown(1);
    }
}
