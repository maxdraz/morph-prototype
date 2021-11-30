using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnHitEffectData : ScriptableObject
{
    [Header("Particles")]
     public GameObject onHitParticles;

    public abstract OnHitEffect CreateOnHitEffectInstance();

}
