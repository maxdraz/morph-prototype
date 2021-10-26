using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMorphAttackHandler : MonoBehaviour
{
   [SerializeField] private AttackQueue appendageAttackQueue;
    private AttackQueue mouthAttackQueue;
    private AttackQueue tailAttackQueue;

    private AttackQueue currentAttackQueue;
    
    //input
    private CreatureVirtualController controller;

    private void Awake()
    {
        controller = GetComponentInParent<CreatureVirtualController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //appendage attack
            TryQueueAttack(ref appendageAttackQueue, true);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            //appendage attack
            TryQueueAttack(ref mouthAttackQueue, false);
        }

        if (currentAttackQueue != null)
        {
            // update current
        }

    }

    void TryQueueAttack(ref AttackQueue queue, bool isLightAttack)
    {
        if (currentAttackQueue == null || !currentAttackQueue.isExectuing) 
        {
            queue.TryQueueAttack(isLightAttack);
        }

        if (IsSameBodyPart(ref queue))
        {
            queue.TryQueueAttack(isLightAttack);
        }
        else
        {
            //check if can transition to other body part
        }
    }

    bool IsSameBodyPart(ref AttackQueue combo)
    {
        // check if any other combo in progress
        if (currentAttackQueue == combo)
        {
            print("same body part");
            return true;
        }
        
        print("different body part");
        return false;

    }

    void SubscribeInputs()
    {
        
    }
    
    void UnsubscribeInputs()
    {
        
    }
}
