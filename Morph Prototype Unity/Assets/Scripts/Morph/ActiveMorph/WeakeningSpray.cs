using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakeningSpray : ActiveMorph
{
    static int chemicalDamagePrerequisite = 35;

    [SerializeField] private RadialProjectileSpawner weakeningSpraySpawner;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
{
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisite),
};

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnWeakeningSpray();
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            SpawnWeakeningSpray();
        }
    }

    private void SpawnWeakeningSpray()
    {
        var projectiles = weakeningSpraySpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }



    private void OnValidate()
    {
        weakeningSpraySpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        weakeningSpraySpawner?.OnDrawGizmos(transform);
    }
}
