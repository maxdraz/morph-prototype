using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActiveMorphHandler))]
public class ActiveMorph : MonoBehaviour
{
    public Prerequisite basePrerequisit;

    [SerializeField] protected float staminaCost;
    [SerializeField] protected float energyCost;

    [SerializeField] protected KeyCode testInput;

    [SerializeField] protected Timer castTimer;
    [SerializeField] protected Timer cooldown;

    private Movement movement;  // TODO - make movement listen to attack handler to change
    private CreatureVirtualController controller;

    [SerializeField] public struct Prerequisite
    {
        string stat;
        int value;

        public Prerequisite(string a, int b)
        {
            stat = a;
            value = b;
        }
    }

    


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
