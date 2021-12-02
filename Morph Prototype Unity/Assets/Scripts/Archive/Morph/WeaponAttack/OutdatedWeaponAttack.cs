using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO - make static method for spawning particles
//TODO - make member vars private! 

[System.Serializable]
public abstract class OutdatedWeaponAttack
{
    [SerializeField] protected float duration;
    private float inputWindowDuringAttack;
    private float inputWindowAfterAttack;
    
    private WeaponOutdatedAttackData _weaponOutdatedAttackData;
    [SerializeField] public List<OutdatedOnHitEffect> onHitEffects;
    private GameObject owner;
    private OutdatedMorph _ownerOutdatedMorph;
    private OutdatedDamageHandler _ownerOutdatedDamageHandler;

    public float Duration => duration;
    public WeaponOutdatedAttackData Data => _weaponOutdatedAttackData;
    public float InputWindowDuringAttack => inputWindowDuringAttack;
    public float InputWindowAfterAttack => inputWindowAfterAttack;

    public OutdatedWeaponAttack(GameObject owner,OutdatedMorph ownerOutdatedMorph, OutdatedDamageHandler ownerOutdatedDamageHandler,WeaponOutdatedAttackData weaponOutdatedAttackData, List<OutdatedOnHitEffect> baseOnHitEffects)
    {
        this.owner = owner;
        this._ownerOutdatedMorph = ownerOutdatedMorph;
        this._ownerOutdatedDamageHandler = ownerOutdatedDamageHandler;
        this._weaponOutdatedAttackData = weaponOutdatedAttackData;
        onHitEffects = baseOnHitEffects;
        duration = weaponOutdatedAttackData.Duration;
        inputWindowDuringAttack = weaponOutdatedAttackData.InputWindowDuringAttack;
        inputWindowAfterAttack = weaponOutdatedAttackData.InputWindowAfterAttack;
    }

    public virtual void ResetAttackData()
    {
        duration = _weaponOutdatedAttackData.Duration;
        inputWindowDuringAttack = _weaponOutdatedAttackData.InputWindowDuringAttack;
        inputWindowAfterAttack = _weaponOutdatedAttackData.InputWindowAfterAttack;
        onHitEffects = _weaponOutdatedAttackData.CreateOnHitEffectInstances(_ownerOutdatedMorph, _ownerOutdatedDamageHandler);
    }
    
    public virtual void OnStart()
    {
        if (_weaponOutdatedAttackData.OnStartParticles)
        {
            var particles = ObjectPooler.Instance.GetOrCreatePooledObject(_weaponOutdatedAttackData.OnStartParticles);
            particles.transform.parent = owner.transform;
            particles.transform.position = owner.transform.position;
            particles.transform.rotation = owner.transform.rotation;
        }
    }

    public virtual void OnHit(OutdatedDamageHandler outdatedDamageTaker)
    {
        // apply onhit effects
        foreach (var onHitEffect in onHitEffects)
        {
            onHitEffect.Apply(outdatedDamageTaker);
        }
        
        // play particles
        var hitParticlePrefab = Data.OnHitParticles;
        if (hitParticlePrefab)
        {
            GameplayStatics.SpawnParticleSystemOnClosestColliderBounds(hitParticlePrefab, owner.transform.position,
                outdatedDamageTaker.GetComponent<Collider>());
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
