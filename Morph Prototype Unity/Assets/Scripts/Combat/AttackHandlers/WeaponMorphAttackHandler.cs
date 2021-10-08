using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMorphAttackHandler : MonoBehaviour
{
   [SerializeField] private ComboHandler appendageComboHandler;
    private ComboHandler mouthComboHandler;
    private ComboHandler tailComboHandler;

    private ComboHandler current;
    
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
            TryExecuteCombo(ref appendageComboHandler, true);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            //appendage attack
            TryExecuteCombo(ref mouthComboHandler, false);
        }

        if (current != null)
        {
            // update current
        }

    }

    void TryExecuteCombo(ref ComboHandler combo, bool isLightAttack)
    {
        if (!current.isExectuing || current == null) 
        {
            combo.TryQueueAttack(isLightAttack);
        }

        if (IsSameBodyPart(ref combo))
        {
            combo.TryQueueAttack(isLightAttack);
        }
        else
        {
            //check if can transition to other body part
        }
        
        
        
        
       
    }

    bool IsSameBodyPart(ref ComboHandler combo)
    {
        // check if any other combo in progress
        if (current == combo)
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
