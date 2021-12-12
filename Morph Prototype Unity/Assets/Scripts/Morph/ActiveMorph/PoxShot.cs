using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoxShot : ActiveMorph
{
    [SerializeField] private EdgeProjectileSpawner poxShotSpawner;

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
           FireProjectiles();
            return true;
        }
        return false;
    }

    private void FireProjectiles()
    {
        var projectiles = poxShotSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }

        GetComponentInParent<Movement>().FaceCameraView = true;
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
