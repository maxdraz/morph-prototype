using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathlyToxins : ActiveMorph
{
    [SerializeField] private RadialProjectileSpawner deathlyToxinsSpawner;

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnDeathlyToxins();
            return true;
        }
        return false;
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(testInput))
        {
            SpawnDeathlyToxins();
        }
    }

    private void SpawnDeathlyToxins()
    {
        var projectiles = deathlyToxinsSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }



    private void OnValidate()
    {
        deathlyToxinsSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        deathlyToxinsSpawner?.OnDrawGizmos(transform);
    }
}
