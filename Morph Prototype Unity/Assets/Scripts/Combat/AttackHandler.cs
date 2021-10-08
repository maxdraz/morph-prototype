using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackHandler : MonoBehaviour
{
    protected CreatureVirtualController controller;

    private void Awake()
    {
        controller = GetComponent<CreatureVirtualController>();
    }

    private void OnEnable()
    
    {
        if (controller)
        {
            controller.AppendageLightAttack += TryQueueAttack;
            controller.AppendageHeavyAttack += TryQueueAttack;
        }
    }

    private void OnDisable()
    {
        if (controller)
        {
            controller.AppendageLightAttack -= TryQueueAttack;
            controller.AppendageHeavyAttack += TryQueueAttack;
        }
    }
    public virtual void SetAttackData(List<LightAttack> lAttacks, List<HeavyAttack> hAttacks){}

    public abstract void TryQueueAttack(bool isLight);
    public abstract void TryQueueAttack(in Attack attack);

}
