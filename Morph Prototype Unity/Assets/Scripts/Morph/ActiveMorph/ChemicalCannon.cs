using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalCannon : ActiveMorph
{
    static int chemicalDamagePrerequisite = 50;

    [SerializeField] private RadialProjectileSpawner chemicalCannonSpawner;


    static Prerequisite[] StatPrerequisits = new Prerequisite[1]
{
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisite),
};

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
            SpawnChemicalCannon();
            return true;
        }
        return false;
    }



    private void Update()
    {
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
