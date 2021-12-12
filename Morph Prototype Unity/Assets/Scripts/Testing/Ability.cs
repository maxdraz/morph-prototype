using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Ability
{
    public LegacyTimer cooldown { get; }

    public Ability(LegacyTimer cooldown)
    {
        this.cooldown = cooldown;
    }
    public abstract void Use();

    public virtual bool IsReady()
    {
        return cooldown.IsFinished();
    }
}
