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
}
