using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackHandler : MonoBehaviour
{
    [SerializeField] protected List<Attack> attackQueue;
    protected Attack currentAttack;
    
    protected bool attackInProgress;
    protected float attackTimer;
    
    public event Action ComboEnded;

    public virtual void TryQueueAttack(bool isLight)
    {
        
    }

    protected virtual void OnComboEnded()
    {
        ComboEnded?.Invoke();
    }
    public abstract bool TryQueueAttack(in Attack attack);

}
