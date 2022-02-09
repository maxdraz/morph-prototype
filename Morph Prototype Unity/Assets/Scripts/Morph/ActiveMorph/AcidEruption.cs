using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEruption : ActiveMorph
{
    static int chemicalDamagePrerequisit = 25;

    [SerializeField] private ConeProjectileSpawner acidEruptionSpawner;

    public Prerequisite[] BasePrerequisits = new Prerequisite[1]
    {
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisit),

    };

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnAcidEruption();
            return true;
        }
        return false;
    }

    private void Update()
    {


        if (Input.GetKeyDown(testInput))
        {
            SpawnAcidEruption();
        }
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
