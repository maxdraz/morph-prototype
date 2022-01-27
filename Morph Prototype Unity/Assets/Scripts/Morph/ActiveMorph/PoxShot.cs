using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoxShot : ActiveMorph
{
    [SerializeField] private ConeProjectileSpawner poxShotSpawner;
    [SerializeField] private RadialProjectileSpawner chemicalCannonSpawner;


    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnPoxShot();
            return true;
        }
        return false;
    }

    private void SpawnPoxShot()
    {
        var projectiles = poxShotSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }

    

    private void OnValidate()
    {
        poxShotSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        poxShotSpawner?.OnDrawGizmos(transform);
    }
}
