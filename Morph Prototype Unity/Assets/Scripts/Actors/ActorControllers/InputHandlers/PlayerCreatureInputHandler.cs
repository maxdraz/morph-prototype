using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreatureInputHandler : CreatureInputHandler
{
    public override bool GetAppendageLightAttackInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    public override bool GetAppendageHeavyAttackInput()
    {
        return Input.GetMouseButtonDown(1);
    }

    public override bool GetMouthLightAttackInput()
    {
        return Input.GetKeyDown(KeyCode.LeftShift) && Input.GetMouseButtonDown(0);
    }

    public override bool GetMouthHeavyAttackInput()
    {
        return Input.GetKeyDown(KeyCode.LeftShift) && Input.GetMouseButtonDown(1);
    }

    public override bool GetTailLightAttackInput()
    {
        return Input.GetKeyDown(KeyCode.LeftControl) && Input.GetMouseButtonDown(0);
    }

    public override bool GetTailHeavyAttackInput()
    {
        return Input.GetKeyDown(KeyCode.LeftControl) && Input.GetMouseButtonDown(1);
    }
}
