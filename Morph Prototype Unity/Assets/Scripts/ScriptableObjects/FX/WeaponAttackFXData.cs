using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAttackFXData : ScriptableObject
{
    [Header("Particles")]
    public GameObject OnStartParticles;
    public GameObject OnUpdateParticles;
    public GameObject OnFinishParticles;
    public GameObject OnHitParticles;
    // public GameObject 
}
