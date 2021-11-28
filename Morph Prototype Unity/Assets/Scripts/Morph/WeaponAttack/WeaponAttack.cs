using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO - make static method for spawning particles
//TODO - make member vars private! 

[System.Serializable]
public abstract class WeaponAttack
{
    [SerializeField] private float duration;
    private float inputWindowDuringAttack;
    private float inputWindowAfterAttack;
    
    private WeaponAttackData weaponAttackData;
    [SerializeField] public List<OnHitEffect> onHitEffects;
    private GameObject owner;

    public float Duration => duration;
    public WeaponAttackData Data => weaponAttackData;
    public float InputWindowDuringAttack => inputWindowDuringAttack;
    public float InputWindowAfterAttack => inputWindowAfterAttack;

    public WeaponAttack(GameObject owner,WeaponAttackData weaponAttackData, List<OnHitEffect> baseOnHitEffects)
    {
        this.owner = owner;
        this.weaponAttackData = weaponAttackData;
        onHitEffects = baseOnHitEffects;
        duration = weaponAttackData.Duration;
        inputWindowDuringAttack = weaponAttackData.InputWindowDuringAttack;
        inputWindowAfterAttack = weaponAttackData.InputWindowAfterAttack;

    }

    public virtual void Reset()
    {
        duration = weaponAttackData.Duration;
        inputWindowDuringAttack = weaponAttackData.InputWindowDuringAttack;
        inputWindowAfterAttack = weaponAttackData.InputWindowAfterAttack;
        onHitEffects = weaponAttackData.CreateOnHitEffectInstances();
    }

    public virtual void OnHit(DamageHandler otherDamageHandler, Collider other)
    {
        
        
        // spawn particles
        var hitParticlePrefab = Data.OnHitParticles;
        if (hitParticlePrefab)
        {
            var hitParticles = ObjectPooler.Instance.GetOrCreatePooledObject(hitParticlePrefab);
            var ownerPos = owner.transform.position;
            var impactPoint =  other.ClosestPointOnBounds(ownerPos);
            hitParticles.transform.position = impactPoint;
            var toImpactPointNormalised = (impactPoint - ownerPos).normalized;
            hitParticles.transform.rotation = Quaternion.LookRotation(-toImpactPointNormalised);
        }
           

    }

    public virtual void OnUpdate()
    {
        
    }

    public virtual void OnFinish()
    {
      
    }

    public virtual void OnStart()
    {
        if (weaponAttackData.OnStartParticles)
        {
            var particles = ObjectPooler.Instance.GetOrCreatePooledObject(weaponAttackData.OnStartParticles);
            particles.transform.parent = owner.transform;
            particles.transform.position = owner.transform.position;
        }
    }
    

}
