using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class PhysicsDebuff
{
    [SerializeField] protected float duration;
    protected float elapsed;
    protected DamageHandler damageTaker;
    protected DamageHandler damageDealer;
    
    public float Duration=> duration;
    public float Elapsed => elapsed;

    public PhysicsDebuff(float duration = 0, DamageHandler damageTaker= null, DamageHandler damageDealer = null)
    {
        this.duration = duration;
        this.damageTaker = damageTaker;
        this.damageDealer = damageDealer;

        elapsed = duration;
    }
    
    
    public virtual void OnFixedUpdate(float deltaTime)
    {
        elapsed -= deltaTime;
        ApplyDebuff();
    }

    protected abstract void ApplyDebuff();

    public virtual bool CheckIfFinished()
    {
        return elapsed <= 0;
    }
}
