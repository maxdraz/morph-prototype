using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack : ICloneable
{
    [SerializeField] protected List<OnHitEffectDataContainer> onHitEffects;
    public List<OnHitEffectDataContainer> OnHitEffects => onHitEffects;

    public Attack(List<OnHitEffectDataContainer> onHitEffects = null)
    {
        this.onHitEffects = onHitEffects;
    }

    public virtual object Clone()
    {
        List<OnHitEffectDataContainer> onHitEffectContainers = new List<OnHitEffectDataContainer>();
        foreach (var onHitEffect in onHitEffects)
        {
            onHitEffectContainers.Add(onHitEffect.Clone() as OnHitEffectDataContainer);
        }
        return new Attack(onHitEffectContainers);
    }
}
