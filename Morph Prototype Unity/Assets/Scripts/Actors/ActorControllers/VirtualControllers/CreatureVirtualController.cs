using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureVirtualController : VirtualController
{
    private CreatureInputHandler inputHandler;

    public event Action LightAttack;
    public event Action HeavyAttack;

    private void Awake()
    {
        inputHandler = GetComponentInChildren<CreatureInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LightAttackInput())
        {
            LightAttack?.Invoke();
        }

        if (HeavyAttackInput())
        {
            HeavyAttack?.Invoke();
        }
    }

    private bool LightAttackInput()
    {
        return inputHandler.GetLightAttackInput();
    }

    bool HeavyAttackInput()
    {
        return inputHandler.GetHeavyAttackInput();
    }
}
