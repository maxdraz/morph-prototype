using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Timer
{
    [SerializeField] private float duration;
    [SerializeField] private float currentTime;
    private bool restart;
    [SerializeField] private bool loop;

    public bool Completed => currentTime <= 0;
    public float CurrentTime => currentTime;
    public float Duration => duration;
    public bool Loop => loop;
    public float TimeElapsed => duration - currentTime;

    public bool JustCompleted
    {
        get;
        private set;
    }

    public bool JustStarted
    {
        get;
        private set;
    }

    public Timer(float duration = 1f, bool loop = false, bool startImmediatedly = false)
    {
        this.duration = duration;
        this.loop = loop;
        currentTime = 0;
        restart = startImmediatedly;
        JustCompleted = false;
        JustStarted = startImmediatedly;
    }

    public void Update(float dt)
    {
        if(Completed)
        {
            JustCompleted = false;
            
            if (loop)
                restart = true;
            
            if(!restart)
                return;
            currentTime = duration;
            restart = false;
            JustStarted = true;
        }
        
        currentTime -= dt;
        currentTime = Mathf.Max(0, currentTime);
        if (JustStarted) JustStarted = false;
        if (Completed) JustCompleted = true;
    }

    public bool RestartIfCompleted()
    {
        if(!Completed)
            return false;
        
        restart = true;
        return true;
    }
}
