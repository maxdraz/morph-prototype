using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonNeedleBarrage : ActiveMorph
{
    [SerializeField] private RadialProjectileSpawner poisonNeedleBarrageSpawner;

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnPoisonNeedleBarrage();
            return true;
        }
        return false;
    }

    private void SpawnPoisonNeedleBarrage()
    {
        var projectiles = poisonNeedleBarrageSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }

    

    private void OnValidate()
    {
        poisonNeedleBarrageSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        poisonNeedleBarrageSpawner?.OnDrawGizmos(transform);
    }
}
