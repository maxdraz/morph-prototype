using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalCannon : ActiveMorph
{
    [SerializeField] private RadialProjectileSpawner chemicalCannonSpawner;

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnChemicalCannon();
            return true;
        }
        return false;
    }

    

    private void SpawnChemicalCannon()
    {
        var projectiles = chemicalCannonSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }

    private void OnValidate()
    {
        chemicalCannonSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        chemicalCannonSpawner?.OnDrawGizmos(transform);
    }
}
