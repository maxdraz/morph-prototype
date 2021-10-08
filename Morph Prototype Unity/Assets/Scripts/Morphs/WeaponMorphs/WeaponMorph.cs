using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public abstract class WeaponMorph : MonoBehaviour
{
    protected AttackHandler attackHandler;
    protected CreatureVirtualController controller;
    protected List<LightAttack> lightAttacks;
    protected List<HeavyAttack> heavyAttacks;

    [SerializeField] protected int lightComboIndex;
    [SerializeField] protected int heavyComboIndex;
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        var existingWeaponMorphs = GetComponents(typeof(WeaponMorph)).ToList();
        if (existingWeaponMorphs.Count > 1)
        {
            Destroy(existingWeaponMorphs[0]);
        }
        
        attackHandler = GetComponent<AttackHandler>();
        controller = GetComponent<CreatureVirtualController>();
        
        InitializeAttacks();
    }
    
    private void OnEnable()
    {
        SubscibeEvents();
    }

    private void OnDisable()
    {
       UnsubscibeEvents();
    }

    protected virtual void OnLightAttack()
    {
        if (lightAttacks.Count < 1)
            return;

        lightComboIndex %= lightAttacks.Count;
        
        if (attackHandler.TryQueueAttack(lightAttacks[lightComboIndex]))
        {
            lightComboIndex++;

            if (heavyComboIndex > 0)
            {
                heavyComboIndex = 0;
            }
        }
    }

    protected virtual void OnHeavyAttack()
    {
        if (heavyAttacks.Count < 1)
            return;
        
        heavyComboIndex %= heavyAttacks.Count;
        
        if (attackHandler.TryQueueAttack(heavyAttacks[heavyComboIndex]))
        {
            heavyComboIndex++;

            if (lightComboIndex > 0)
            {
                lightComboIndex = 0;
            }
        }
    }

    private void OnComboEnd()
    {
        lightComboIndex = 0;
        heavyComboIndex = 0;
    }

    protected abstract void InitializeAttacks();

    protected virtual void SubscibeEvents()
    {
        if (controller)
        {
            controller.AppendageLightAttack += OnLightAttack;
            controller.AppendageHeavyAttack += OnHeavyAttack;
        }

        if (attackHandler)
        {
            attackHandler.ComboEnded += OnComboEnd;
        }
    }
    
    protected virtual void UnsubscibeEvents()
    {
        if (controller)
        {
            controller.AppendageLightAttack -= OnLightAttack;
            controller.AppendageHeavyAttack -= OnHeavyAttack;
        }
        
        if (attackHandler)
        {
            attackHandler.ComboEnded -= OnComboEnd;
        }
    }

}
