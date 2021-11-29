using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureVirtualController : VirtualController
{
    private CreatureInputHandler inputHandler;

    public event Action AppendageLightAttack;
    public event Action AppendageHeavyAttack;
    public event Action MouthLightAttack;
    public event Action MouthHeavyAttack;
    public event Action TailLightAttack;
    public event Action TailHeavyAttack;

    private void Awake()
    {
        inputHandler = GetComponentInChildren<CreatureInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AppendageLightAttackInput())
        {
            AppendageLightAttack?.Invoke();
        }

        if (AppendageHeavyAttackInput())
        {
            AppendageHeavyAttack?.Invoke();
        }
        if (MouthLightAttackInput())
        {
            MouthLightAttack?.Invoke();
        }

        if (MouthHeavyAttackInput())
        {
            MouthHeavyAttack?.Invoke();
        }
        
        if (TailLightAttackInput())
        {
            TailLightAttack?.Invoke();
        }

        if (TailHeavyAttackInput())
        {
            TailHeavyAttack?.Invoke();
        }
        
    }

    private bool AppendageLightAttackInput()
    {
        return inputHandler.GetAppendageLightAttackInput();
    }

    bool AppendageHeavyAttackInput()
    {
        return inputHandler.GetAppendageHeavyAttackInput();
    }
    
    private bool MouthLightAttackInput()
    {
        return inputHandler.GetMouthLightAttackInput();
    }

    bool MouthHeavyAttackInput()
    {
        return inputHandler.GetMouthHeavyAttackInput();
    }
    
    private bool TailLightAttackInput()
    {
        return inputHandler.GetTailLightAttackInput();
    }

    bool TailHeavyAttackInput()
    {
        return inputHandler.GetTailHeavyAttackInput();
    }
}
