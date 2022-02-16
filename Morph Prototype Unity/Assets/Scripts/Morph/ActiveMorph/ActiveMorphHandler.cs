using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MorphLoadout),typeof(AttackAndAbilityHandler))]
public class ActiveMorphHandler : MonoBehaviour
{
    private CreatureVirtualController controller;
    private MorphLoadout morphLoadout;

    private AttackAndAbilityHandler attackAndAbilityHandler;

    public event Action ActiveMorphActivated;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponentInParent<CreatureVirtualController>();
        morphLoadout = GetComponent<MorphLoadout>();
        attackAndAbilityHandler = GetComponent<AttackAndAbilityHandler>();
    }

    private void OnEnable()
    {
        controller.UseAbility1 += OnUseAbility1;
        controller.UseAbility2 += OnUseAbility2;
        controller.UseAbility3 += OnUseAbility3;
        controller.UseAbility4 += OnUseAbility4;
    }

    private void OnDisable()
    {
        controller.UseAbility1 -= OnUseAbility1;
        controller.UseAbility2 -= OnUseAbility2;
        controller.UseAbility3 -= OnUseAbility3;
        controller.UseAbility4 -= OnUseAbility4;
    }
    private void OnUseAbility1()
    {
        var activeMorph = morphLoadout.GetActiveMorph(0);
        if (activeMorph)
        {
            if (activeMorph.ActivateIfConditionsMet())
            {
                ActiveMorphActivated?.Invoke();
            }
        }
        
    }
    private void OnUseAbility2()
    {
        morphLoadout.GetActiveMorph(1)?.ActivateIfConditionsMet();
    }
    private void OnUseAbility3()
    {
        morphLoadout.GetActiveMorph(2)?.ActivateIfConditionsMet();
    }
    private void OnUseAbility4()
    {
        morphLoadout.GetActiveMorph(3)?.ActivateIfConditionsMet();
    }
}
