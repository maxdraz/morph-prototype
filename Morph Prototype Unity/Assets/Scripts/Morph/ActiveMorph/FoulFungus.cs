using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoulFungus : ActiveMorph
{
    static int chemicalDamagePrerequisit = 30;

    public Prerequisite[] StatPrerequisits;

    [SerializeField] private RadialProjectileSpawner foulFungusGasCloudSpawner;

    private void Start()
    {
        WriteToPrerequisiteArray();
    }

    void WriteToPrerequisiteArray()
    {
        statPrerequisits = new Prerequisite[StatPrerequisits.Length];

        for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
        {
            statPrerequisits[i] = StatPrerequisits[i];
            Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
        }
    }

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
