using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO - make static method for spawning particles
//TODO - make member vars private! 

[System.Serializable]
public abstract class WeaponAttack
{
    [SerializeField] protected float duration;
    private float inputWindowDuringAttack;
    private float inputWindowAfterAttack;
    
    private WeaponAttackData weaponAttackData;
    [SerializeField] public List<OnHitEffect> onHitEffects;
    private GameObject owner;
    private Morph ownerMorph;
    private DamageHandler ownerDamageHandler;

    public float Duration => duration;
    public WeaponAttackData Data => weaponAttackData;
    public float InputWindowDuringAttack => inputWindowDuringAttack;
    public float InputWindowAfterAttack => inputWindowAfterAttack;

    public WeaponAttack(GameObject owner,Morph ownerMorph, DamageHandler ownerDamageHandler,WeaponAttackData weaponAttackData, List<OnHitEffect> baseOnHitEffects)
    {
        this.owner = owner;
        this.ownerMorph = ownerMorph;
        this.ownerDamageHandler = ownerDamageHandler;
        this.weaponAttackData = weaponAttackData;
        onHitEffects = baseOnHitEffects;
        duration = weaponAttackData.Duration;
        inputWindowDuringAttack = weaponAttackData.InputWindowDuringAttack;
        inputWindowAfterAttack = weaponAttackData.InputWindowAfterAttack;
    }

    public virtual void ResetAttackData()
    {
        duration = weaponAttackData.Duration;
        inputWindowDuringAttack = weaponAttackData.InputWindowDuringAttack;
        inputWindowAfterAttack = weaponAttackData.InputWindowAfterAttack;
        onHitEffects = weaponAttackData.CreateOnHitEffectInstances(ownerMorph, ownerDamageHandler);
    }
    
    public virtual void OnStart()
    {
        if (weaponAttackData.OnStartParticles)
        {
            var particles = ObjectPooler.Instance.GetOrCreatePooledObject(weaponAttackData.OnStartParticles);
            particles.transform.parent = owner.transform;
            particles.transform.position = owner.transform.position;
            particles.transform.rotation = owner.transform.rotation;
        }
    }

    public virtual void OnHit(DamageHandler damageTaker)
    {
        // apply onhit effects
        foreach (var onHitEffect in onHitEffects)
        {
            onHitEffect.Apply(damageTaker);
        }
        
        // play particles
        var hitParticlePrefab = Data.OnHitParticles;
        if (hitParticlePrefab)
        {
            GameplayStatics.SpawnParticleSystemOnClosestColliderBounds(hitParticlePrefab, owner.transform.position,
                damageTaker.GetComponent<Collider>());
        }
    }
    
    public virtual void OnUpdate()
    {
        
    }

    public virtual void OnFinish()
    {
        var finishParticlePrefab = Data.OnFinishParticles;
        if (finishParticlePrefab)
        {
            GameplayStatics.SpawnParticleSystem(finishParticlePrefab, owner.transform, owner.transform.position,
                owner.transform.rotation);
        }
        
        ResetAttackData();
    }
}
