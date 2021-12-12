using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActiveMorphHandler))]
public class ActiveMorph : MonoBehaviour
{
    [SerializeField] protected Timer castTimer;
    [SerializeField] protected Timer LookInCameraViewTimer;
    [SerializeField] protected Timer cooldown;

    private Movement movement;  // TODO - make movement listen to attack handler to change

    private void Awake()
    {
        movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        castTimer.Update(Time.deltaTime);
        LookInCameraViewTimer.Update(Time.deltaTime);
        cooldown.Update(Time.deltaTime);
        
        if (LookInCameraViewTimer.JustStarted)
        {
            print("just started");
            movement.FaceCameraView = true;
        }

        if (LookInCameraViewTimer.JustCompleted)
        {
            print("just finished");
            movement.FaceCameraView = false;
        }
    }

    public virtual bool ActivateIfConditionsMet()
    {
        return LookInCameraViewTimer.RestartIfCompleted() ||  cooldown.RestartIfCompleted();
    }

    private bool LookInCameraView()
    {
        if (LookInCameraViewTimer.Completed)
            movement.FaceCameraView = false;
        else
        {
            movement.FaceCameraView = true;
            LookInCameraViewTimer.RestartIfCompleted();
        }

        return LookInCameraViewTimer.Completed;

    }
}
