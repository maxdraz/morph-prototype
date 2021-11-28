using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Physical", menuName = "Weapon Morph/On Hit Effect Data/Physical")]
public class PhysicalOnHitEffectData : OnHitEffectData
{
    [SerializeField] private float damage;

    public float Damage => damage;
    
    public override OnHitEffect CreateOnHitEffectInstance()
    {
        return new PhysicalOnHitEffect(this);
    }
}
