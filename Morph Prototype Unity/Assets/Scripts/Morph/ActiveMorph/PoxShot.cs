using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoxShot : ActiveMorph
{
    [SerializeField] private ConeProjectileSpawner poxShotSpawner;

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
        SpendEnergy(energyCost);
        SpendStamina(staminaCost);
        var projectiles = poxShotSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(testInput))
        {
            SpawnPoxShot();
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
