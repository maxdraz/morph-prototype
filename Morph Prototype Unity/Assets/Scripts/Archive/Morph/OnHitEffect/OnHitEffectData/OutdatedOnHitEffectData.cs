using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OutdatedOnHitEffectData : ScriptableObject
{
    [Header("Particles")]
     public GameObject onHitParticles;

    public abstract OutdatedOnHitEffect CreateOnHitEffectInstance(OutdatedMorph owner, OutdatedDamageHandler ownerOutdatedDamageHandler);
    

}
