using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class OnHitEffectDataContainer : ICloneable
{
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
