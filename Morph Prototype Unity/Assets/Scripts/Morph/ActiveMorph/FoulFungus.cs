using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoulFungus : ActiveMorph
{
    [SerializeField] private RadialProjectileSpawner foulFungusGasCloudSpawner;

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnGasCloud();
            return true;
        }
        return false;
    }

    private void SpawnGasCloud()
    {
        var projectiles = foulFungusGasCloudSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }

    private void OnValidate()
    {
        foulFungusGasCloudSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        foulFungusGasCloudSpawner?.OnDrawGizmos(transform);
    }
}
