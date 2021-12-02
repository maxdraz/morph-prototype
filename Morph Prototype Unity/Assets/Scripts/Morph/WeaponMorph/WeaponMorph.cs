using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMorph : Morph
{
    [SerializeField] private float baseDamage = 10;
    private int currentLightAttack;
    private int currentHeavyAttack;
    
    [SerializeField] private List<LightAttack> lightAttacks;
    [SerializeField] private List<HeavyAttack> heavyAttacks;

    // Start is called before the first frame update
    void Awake()
    {
        AddMorphDamageToOnHitEffects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //attack handling
    public WeaponAttack GetCurrentAttack(bool isLightAttack)
    {
        if (isLightAttack)
        {
            if (currentLightAttack >= lightAttacks.Count) return null;
            return lightAttacks[currentLightAttack++];
        }
        
        if (currentHeavyAttack >= heavyAttacks.Count) return null;
        return heavyAttacks[currentHeavyAttack++];
    }
    public void ResetCombo()
    {
        currentLightAttack = 0;
        currentHeavyAttack = 0;
    }
    // on hit effects handling
    private void AddMorphDamageToOnHitEffects()
    {
        var physicalDamageOnHitEffects = GetAllOnHitEffectsOfType<IPhysicalDamage>();
        foreach (var physicalDamageOnHitEffect in physicalDamageOnHitEffects)
        {
            if (physicalDamageOnHitEffect.Data is IPhysicalDamage physicalDamage)
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
        print("morphs found : " + allOnHitEffectsOfType.Count);
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
