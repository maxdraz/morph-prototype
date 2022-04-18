using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PullTowardsDebuff : PhysicsDebuff
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float pullForce;
    [SerializeField] protected ForceMode forceMode;
    
    public Transform Target => target;
    public float PullForce => pullForce;
    public ForceMode ForceMode => forceMode;
    
    public PullTowardsDebuff
        (float pullForce = 100, ForceMode forceMode = ForceMode.Acceleration, Transform target = null, float duration = 0, DamageHandler damageTaker = null, DamageHandler damageDealer = null) 
        : base(duration, damageTaker, damageDealer)
    {
        this.pullForce = pullForce;
        this.forceMode = forceMode;
        this.target = target;
    }

    protected override void ApplyDebuff()
    {
        var direction = (target.position- damageTaker.transform.position).normalized;
        damageTaker.ParentRigidbody.AddForce(direction * pullForce, forceMode);
    }
}
