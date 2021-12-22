using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActiveMorphHandler))]
public class ActiveMorph : MonoBehaviour
{
    [SerializeField] protected Timer castTimer;
    [SerializeField] protected Timer cooldown;

    private Movement movement;  // TODO - make movement listen to attack handler to change
    private CreatureVirtualController controller;

    private void Awake()
    {
        movement = GetComponentInParent<Movement>();
        controller = GetComponentInParent<CreatureVirtualController>();
    }

    private void Update()
    {
        castTimer.Update(Time.deltaTime);
        cooldown.Update(Time.deltaTime);

    }

    public virtual bool ActivateIfConditionsMet()
    {
        bool shouldActivate = cooldown.RestartIfCompleted();

        if (shouldActivate)
        {
            controller.CharacterRotator.StartRotating(
                CharacterRotationMode.CameraForward, 
                CharacterRotationMode.Velocity, 
                new Timer(1f));
        }

        return shouldActivate;
    }
}
