using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.Serializable]
public class Timer
{
    [SerializeField] private float startTime;
    [SerializeField] private float currentTime;

    public Timer(float startTime)
    {
        this.startTime = startTime;
        currentTime = 0;
    }

    public bool IsFinished()
    {
        if (currentTime <= startTime)
        {
            Tick();
        }
        
        return currentTime <= 0;
    }

    public void Tick()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }

    public void Start()
    {
        currentTime = startTime;
    }

    
}

