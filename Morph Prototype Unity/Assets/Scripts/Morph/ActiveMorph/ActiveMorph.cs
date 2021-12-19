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
        if (LookInCameraViewTimer.JustStarted)
        {
            print("just started");
          //  movement.RotateToCameraView(1);
        }
        castTimer.Update(Time.deltaTime);
        LookInCameraViewTimer.Update(Time.deltaTime);
        cooldown.Update(Time.deltaTime);
        
        

        if (LookInCameraViewTimer.JustCompleted)
        {
            print("just finished");
           // movement.StopFacingCameraView();
        }
    }

    public virtual bool ActivateIfConditionsMet()
    {
        return cooldown.RestartIfCompleted();
    }
}
