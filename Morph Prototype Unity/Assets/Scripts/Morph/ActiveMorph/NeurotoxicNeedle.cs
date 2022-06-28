using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeurotoxicNeedle : ActiveMorph
{
    static int chemicalDamagePrerequisite = 50;

    [SerializeField] private RadialProjectileSpawner neurotoxicNeedleSpawner;

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnNeurotoxicNeedle();
            return true;
        }
        return false;
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(testInput))
        {
            SpawnNeurotoxicNeedle();
        }
    }

    private void SpawnNeurotoxicNeedle()
    {
        var projectiles = neurotoxicNeedleSpawner?.Spawn(transform);

        if (projectiles != null)
            foreach (var projectile in projectiles)
            {
                projectile.GetComponent<Projectile>().SetDamageDealer(GetComponent<DamageHandler>());
            }
    }

    private void OnValidate()
    {
        neurotoxicNeedleSpawner?.OnValidate();
    }

    private void OnDrawGizmos()
    {
        neurotoxicNeedleSpawner?.OnDrawGizmos(transform);
    }
}
