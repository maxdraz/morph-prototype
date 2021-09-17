using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Ability
{
    public Timer cooldown { get; }

    public Ability(Timer cooldown)
    {
        this.cooldown = cooldown;
    }
    public abstract void Use();

    public virtual bool IsReady()
    {
        return cooldown.IsFinished();
    }
}
