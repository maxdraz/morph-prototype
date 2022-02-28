using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoulFungus : ActiveMorph
{
    static int chemicalDamagePrerequisit = 30;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisit),
    };

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

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            SpawnGasCloud();
        }
    }

    private void SpawnGasCloud()
    {
        var projectiles = foulFungusGasCloudSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<AOE_DOT>().SetDamageDealer(GetComponent<DamageHandler>());
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
