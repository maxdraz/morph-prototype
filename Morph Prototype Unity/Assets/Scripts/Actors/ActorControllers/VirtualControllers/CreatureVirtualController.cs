using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureVirtualController : VirtualController
{
    private CreatureInputHandler inputHandler;

    public event Action<bool> AppendageLightAttack;
    public event Action<bool> AppendageHeavyAttack;

    private void Awake()
    {
        inputHandler = GetComponentInChildren<CreatureInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AppendageLightAttackInput())
        {
            AppendageLightAttack?.Invoke(true);
        }

        if (AppendageHeavyAttackInput())
        {
            AppendageHeavyAttack?.Invoke(false);
        }
    }

    private bool AppendageLightAttackInput()
    {
        return inputHandler.GetLightAttackInput();
    }

    bool AppendageHeavyAttackInput()
    {
        return inputHandler.GetHeavyAttackInput();
    }
}
