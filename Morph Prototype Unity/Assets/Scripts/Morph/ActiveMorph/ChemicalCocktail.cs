using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalCocktail : ActiveMorph
{
    static int chemicalDamagePrerequisite = 50;
    [SerializeField] private GameObject chemicalCocktailParticle;
    [SerializeField] private float explosionDelay;
    [SerializeField] private float poisonStackModifier;

    [SerializeField] private RadialProjectileSpawner viscousBlastSpawner;
    
    private void Start()
    {
        damageHandler = GetComponent<DamageHandler>();
    }

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnChemicalCocktail();
            return true;
        }
        return false;
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(testInput))
        {
            SpawnChemicalCocktail();
        }
    }

    private void SpawnChemicalCocktail()
    {
        GameObject chemicalCocktail = Instantiate(chemicalCocktailParticle,transform.position, transform.rotation);
        chemicalCocktail.GetComponent<AOE_DELAY>().StartCoroutine("TriggerActivation");
        chemicalCocktail.transform.position = transform.position;
        chemicalCocktail.transform.parent = this.gameObject.transform;

        Invoke("ChemicalCocktailDamage", explosionDelay);

    }

    private void ChemicalCocktailDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Stats>() == true)
            if (hitCollider.GetComponent<DebuffHandler>().PoisonStack == 0)
            {
                return;
            }
            else
            {
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new PoisonDamageData(hitCollider.GetComponent<DebuffHandler>().PoisonStack * poisonStackModifier), damageHandler);
            }
        }
    }
}
