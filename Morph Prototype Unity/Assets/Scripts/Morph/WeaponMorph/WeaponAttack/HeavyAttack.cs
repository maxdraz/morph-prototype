using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeavyAttack : WeaponAttack
{
    [SerializeField] private float chargeUpTime;
    [SerializeField] private HeavyAttackFXData heavyAttackFX;

    public HeavyAttack(float chargeUpTime = 0.5f, HeavyAttackFXData heavyAttackFX = null, WeaponAttack weaponAttack = null)
        :base(weaponAttack)
    {
        this.chargeUpTime = chargeUpTime;
        this.heavyAttackFX = heavyAttackFX;
    }

    public override void OnStart()
    {
        base.OnStart();

        var transform = Owner.transform;
        GameplayStatics.SpawnParticleSystem(heavyAttackFX.OnStartParticles, transform, transform.position,
            transform.rotation);
    }

    public override void OnHit(DamageHandler damageTaker, DamageHandler damageDealer)
    {
        base.OnHit(damageTaker, damageDealer);

        GameplayStatics.SpawnParticleSystemOnClosestColliderBounds(
            heavyAttackFX.OnHitParticles,
            Owner.transform.position,
            damageTaker.GetComponent<Collider>(),
            0.3f);
    }

    public override object Clone()
    {
       var weaponAttack = base.Clone() as WeaponAttack;
       return new HeavyAttack(chargeUpTime, heavyAttackFX, weaponAttack);
    }
}
