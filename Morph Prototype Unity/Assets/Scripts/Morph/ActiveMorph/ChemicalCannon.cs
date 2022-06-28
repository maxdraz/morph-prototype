using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalCannon : ActiveMorph
{
    static int chemicalDamagePrerequisite = 50;

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



    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(testInput))
        {
            SpawnChemicalCannon();
        }
    }

    private void SpawnChemicalCannon()
    {
        SpendEnergy(energyCost);
        SpendStamina(staminaCost);

        var projectiles = chemicalCannonSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
                projectile.GetComponent<ChemicalCannonProjectile>().source = gameObject.GetComponent<DamageHandler>();
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
