using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathlyToxins : ActiveMorph
{
    static int chemicalDamagePrerequisite = 30;

    [SerializeField] private RadialProjectileSpawner deathlyToxinsSpawner;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisite),
    };

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnDeathlyToxins();
            return true;
        }
        return false;
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
