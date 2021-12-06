using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightAttack : WeaponAttack
{
    [SerializeField] private LightAttackFXData lightAttackFX;
    
    public LightAttack( LightAttackFXData lightAttackFX = null, WeaponAttack weaponAttack = null)
        :base(weaponAttack)
    {
        this.lightAttackFX = lightAttackFX;
    }

    public override void OnStart()
    {
        base.OnStart();

        if (lightAttackFX){
            var particleSystemGO = GameplayStatics.SpawnParticleSystem(lightAttackFX.OnStartParticles, Owner.transform);
            
            var psController = particleSystemGO.GetComponentInParent<ParticleSystemController>();
            if (psController)
            {
                psController.ScaleParticlesToDuration(duration);
            }
        }
}

    public override void OnHit(DamageHandler damageTaker, DamageHandler damageDealer)
    {
        base.OnHit(damageTaker, damageDealer);

        if (lightAttackFX)
        {
            var particleSystemGO = GameplayStatics.SpawnParticleSystemOnClosestColliderBounds(
                lightAttackFX.OnHitParticles,
                Owner.transform.position,
                damageTaker.GetComponent<Collider>(),
                0.3f);
        }
    }

    public override object Clone()
    {
        var weaponAttack = base.Clone() as WeaponAttack;
        return new LightAttack(lightAttackFX, weaponAttack);
    }
}
