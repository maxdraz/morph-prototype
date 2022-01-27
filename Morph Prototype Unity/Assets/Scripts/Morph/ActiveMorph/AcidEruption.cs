using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEruption : ActiveMorph
{
    [SerializeField] private ConeProjectileSpawner acidEruptionSpawner;

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnAcidEruption();
            return true;
        }
        return false;
    }

    private void SpawnAcidEruption()
    {
        var projectiles = acidEruptionSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }



    private void OnValidate()
    {
        acidEruptionSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        acidEruptionSpawner?.OnDrawGizmos(transform);
    }
}
