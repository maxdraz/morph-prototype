using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class TestAttack
{
    public event Action Started;
    public Action Ended;
    public int index;
    public float duration;
    public float currentTime;
    public bool CanAcceptInput;
    

    public TestAttack(int index, float duration)
    {
        this.duration = duration;
        this.index = index;
        currentTime = duration;
        CanAcceptInput = true;

    }
    
    public void Start()
    {
        Started?.Invoke();
        CanAcceptInput = false;
    }

    public void Update()
    {
        currentTime -= Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0, duration);

        if (currentTime <= 0)
        {
            End();
        }
        
    }

    private void End()
    {
        Ended?.Invoke();
        CanAcceptInput = true;
        currentTime = duration;
    }

    public bool CanQueueNext()
    {
        return currentTime <= 0.5f;
    }
    
}
