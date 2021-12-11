using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMorph : MonoBehaviour
{
    [SerializeField] protected Timer cooldown;

    private void Awake()
    {
        cooldown.Stop();
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Activate();
        }
        
        if(!cooldown.CanCountDown) return;

        cooldown.CountDown(Time.deltaTime);

        if (cooldown.JustFinished)
        {
            print("Can activate again");
        }
    }

    protected virtual bool Activate()
    {
        if (cooldown.CanCountDown)
        {
           return false;
        }
       
        cooldown.Restart();
        return true;

    }
}
