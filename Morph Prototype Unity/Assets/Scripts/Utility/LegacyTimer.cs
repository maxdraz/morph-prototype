using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LegacyTimer
{
    [SerializeField] private float duration;
    [SerializeField] private float currentTime;
    [SerializeField] private bool loop;
    private bool justFinished;
    [SerializeField]private bool canCountDown;
    
    // public interface
    public float CurrentTime
    {
        get => currentTime;
        set => currentTime = value;
    }
    
    public bool JustStarted => duration == currentTime;
    public float Duration
    {
        get => duration;
        set => duration = value;
    }
    public bool Loop {
        get => loop;
        set => loop = value;
    }
    public bool CanCountDown => canCountDown;
    public bool JustFinished => justFinished;

    public float TimeElapsed
    {
        get
        {
            if (duration > 0)
            {
                return duration - currentTime;
            }
            return 0;
        }
    }

    public LegacyTimer(float duration = 1, bool loop = false)
    {
        this.duration = duration;
        this.loop = loop;
        currentTime = duration;
        justFinished = false;
        canCountDown = true;
    }
    
    public bool CountDown(float deltaTime)
    {
        if (!canCountDown)
        {
            justFinished = false;
            if (loop)
            {
                Restart();
            }
            return false;
        }

        currentTime -= deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            Finish();
            return false;
        }
        return true;
    }
    
    private void Finish()
    {
        justFinished = true;
        Stop();
    }
    
    public void Stop()
    {
        canCountDown = false;
    }

    public void Pause()
    {
        canCountDown = false;
    }

    public void Resume()
    {
        canCountDown = true;
    }
    
    public void Restart()
    {
        canCountDown = true;
        justFinished = false;
        currentTime = duration;
    }

    public void Restart(float newDuration)
    {
        duration = newDuration;
        Restart();
    }

    public void Restart(float newDuration, bool shouldLoop)
    {
        loop = shouldLoop;
        Restart(newDuration);
    }

    //public bool JustFinished()
   // {
 //       if (justFinished)
 //       {
    //         justFinished = false;
    //         return true;
    //     }
    //     return false;
    // }

    public bool IsFinished()
    {
        return currentTime <= 0 || justFinished;
    }
}
