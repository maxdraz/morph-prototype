using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureVirtualController : VirtualController
{
    private CreatureInputHandler inputHandler;
    private Movement movement;

    public event Action<Vector2> Movement;

    public event Action LimbLightAttack;
    public event Action LimbHeavyAttack;
    public event Action MouthLightAttack;
    public event Action MouthHeavyAttack;
    public event Action TailLightAttack;
    public event Action TailHeavyAttack;

    public event Action UseAbility1;
    public event Action UseAbility2;
    public event Action UseAbility3;
    public event Action UseAbility4;
    
    private void Awake()
    {
        inputHandler = GetComponentInChildren<CreatureInputHandler>();
        movement = GetComponent<Movement>();
    }

    public void Move(Vector2 moveDir)
    {
        movement.SetMovementDirection(moveDir);
    }

    public void InvokeLimbLightAttack()
    {
        LimbLightAttack?.Invoke();
    }
    public void InvokeLimbHeavyAttack()
    {
        LimbHeavyAttack?.Invoke();
    }
    public void InvokeMouthLightAttack()
    {
        MouthLightAttack?.Invoke();
    }
    public void InvokeMouthHeavyAttack()
    {
        MouthHeavyAttack?.Invoke();
    }
    public void InvokeTailLightAttack()
    {
        TailLightAttack?.Invoke();
    }
    public void InvokeTailHeavyAttack()
    {
        TailHeavyAttack?.Invoke();
    }
    
    public void InvokeUseAbility1()
    {
        UseAbility1?.Invoke();
    }
    public void InvokeUseAbility2()
    {
        UseAbility2?.Invoke();
    }
    public void InvokeUseAbility3()
    {
        UseAbility3?.Invoke();
    }
    public void InvokeUseAbility4()
    {
        UseAbility4?.Invoke();
    }
}
