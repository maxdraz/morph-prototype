using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnHitEffectData : ScriptableObject
{
    [Header("Particles")]
    [SerializeField] private GameObject onHitParticles;

    public abstract OnHitEffect CreateOnHitEffectInstance();

}