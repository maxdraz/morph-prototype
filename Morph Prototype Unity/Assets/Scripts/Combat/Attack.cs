using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Attack
{
    public string name;
    public float duration;
    public float inputNextWindow;
    public float timeUntilComplete;
    public bool completed;
    public bool isLightAttack;
    public bool canComboIntoOtherType;

    protected Attack(float duration, float inputNextWindow = 0.5f, bool canComboIntoOtherType = true)
    {
        this.duration = duration;
        this.inputNextWindow = inputNextWindow;
        this.canComboIntoOtherType = canComboIntoOtherType;

        name = "unnamed attack";
        timeUntilComplete = duration;
    }

    public virtual void Start()
    {
        timeUntilComplete = duration;
        completed = false;
    }

    public virtual void Update()
    {
        if (timeUntilComplete > 0)
        {
            timeUntilComplete -= Time.deltaTime;
            timeUntilComplete = Mathf.Clamp(timeUntilComplete, 0, duration);
        }
        else
        {
            End();
        }
    }

    public virtual void End()
    {
        completed = true;
    }

    public float GetProgress()
    {
        return (duration - timeUntilComplete) / duration;
    }
}
