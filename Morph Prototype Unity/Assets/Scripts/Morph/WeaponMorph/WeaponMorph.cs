using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMorph : Morph
{
    [SerializeField] protected float baseDamage = 10;
    protected int currentLightAttack;
    protected int currentHeavyAttack;
    
    [SerializeField] protected List<LightAttack> lightAttacks;
    [SerializeField] protected List<HeavyAttack> heavyAttacks;


    public List<LightAttack> LightAttacks
    {
        get => lightAttacks;
        set => lightAttacks = value;
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        AddMorphDamageToPhysicalOnHitEffects();
    }

    //attack handling
    public WeaponAttack GetCurrentAttack(bool isLightAttack)
    {
        if (isLightAttack)
        { 
            if (lightAttacks != null && lightAttacks.Count > 0)
                return lightAttacks[currentLightAttack];
        }
        
        if (heavyAttacks != null && heavyAttacks.Count > 0) 
            return heavyAttacks[currentHeavyAttack];

        return null;
    }

    public void AdvanceCombo(bool isLight)
    {
        if (isLight)
        {
            currentLightAttack++;
            currentLightAttack %= lightAttacks.Count;
            return;
        }
        currentHeavyAttack++;
        currentHeavyAttack %= heavyAttacks.Count;
    }

    public void ResetCombo()
    {
        currentLightAttack = 0;
        currentHeavyAttack = 0;
    }
    // on hit effects handling
    private void AddMorphDamageToPhysicalOnHitEffects()
    {
        var onHitEffects = GetAllOnHitEffectsOfType<IPhysicalDamage>();
        foreach (var onHitEffect in onHitEffects)
        {
            if (onHitEffect.Data is IPhysicalDamage physicalDamage)
            {
                physicalDamage.MorphDamage = baseDamage;
            }
        }
    }
    private List<OnHitEffectDataContainer> GetAllOnHitEffects()
    {
        List<OnHitEffectDataContainer> onHitEffectDataContainers = new List<OnHitEffectDataContainer>();

        if (lightAttacks != null)
        {
            foreach (var lightAttack in lightAttacks)
            {
                foreach (var onHitEffect in lightAttack.OnHitEffects)
                {
                    onHitEffectDataContainers.Add(onHitEffect);
                  
                }
            }
        }
        
        if (heavyAttacks != null)
        {
            foreach (var heavyAttack in heavyAttacks)
            {
                foreach (var onHitEffect in heavyAttack.OnHitEffects)
                {
                    onHitEffectDataContainers.Add(onHitEffect);
                }
            }
        }

        return onHitEffectDataContainers;
    }
    private List<OnHitEffectDataContainer> GetAllOnHitEffectsOfType<T>() where T : IDamageType
    {
        var allOnHitEffects = GetAllOnHitEffects();
        List<OnHitEffectDataContainer> allOnHitEffectsOfType = new List<OnHitEffectDataContainer>();
        foreach (var onHitEffect in allOnHitEffects)
        {
            if (onHitEffect.Data is T)
            {
                allOnHitEffectsOfType.Add(onHitEffect);
            }   
        }
        return allOnHitEffectsOfType;
    }
    private void OnValidate()
    {
        var onHitEffectContainers = GetAllOnHitEffects();
        foreach (var onHitEffectDataContainer in onHitEffectContainers)
        {
            onHitEffectDataContainer.OnValidate();
        }
    }
}
