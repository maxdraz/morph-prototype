using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class OnHitEffectDataContainer : ICloneable
{
    public static void OnValidate(ref List<OnHitEffectDataContainer> onHitEffects)
    {
        foreach (var onHitEffect in onHitEffects)
        {
            onHitEffect.OnValidate();
        }
    }

    public static void ApplyOnHitEffects(ref List<OnHitEffectDataContainer> onHitEffects, DamageHandler dmgTaker, DamageHandler dmgDealer)
    {
        foreach (var onHitEffect in onHitEffects)
        {
            onHitEffect.OnHitEffect.ApplyOnHitEffect(onHitEffect.Data, dmgTaker, dmgDealer);
        }
    }
    
    [SerializeField] private OnHitEffect onHitEffect;
    [SerializeReference] private OnHitEffectData data;

    public OnHitEffect OnHitEffect => onHitEffect;
    public OnHitEffectData Data => data;

    public OnHitEffectDataContainer(OnHitEffect onHitEffect = null, OnHitEffectData data = null)
    {
        this.onHitEffect = onHitEffect;
        this.data = data;
    }
    

    public void OnValidate()
    {
        if (onHitEffect == null)
        {
            data = null;
            return;
        }

        if (!onHitEffect.GetData().GetType().IsInstanceOfType(data))
        {
            data = onHitEffect.GetData();
        }
    }

    public object Clone()
    {
        return new OnHitEffectDataContainer(onHitEffect, data.Clone() as OnHitEffectData);
    }
}
