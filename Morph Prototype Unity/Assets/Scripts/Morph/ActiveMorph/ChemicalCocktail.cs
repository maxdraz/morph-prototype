using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalCocktail : ActiveMorph
{
    static int chemicalDamagePrerequisite = 50;
    [SerializeField] private GameObject chemicalCocktailParticle;
    [SerializeField] private float explosionDelay;
    [SerializeField] private float poisonStackModifier;
    DamageHandler damageHandler;

    [SerializeField] private RadialProjectileSpawner viscousBlastSpawner;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
{
        new Prerequisite("chemicalDamage", chemicalDamagePrerequisite),
};



    private void Start()
    {
        damageHandler = GetComponent<DamageHandler>();
    }

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnChemicalCocktail();
            Invoke("ChemicalCocktailDamage", explosionDelay);
            return true;
        }
        return false;
    }

    private void SpawnChemicalCocktail()
    {
        GameObject chemicalCocktail = Instantiate(chemicalCocktailParticle,transform.position, transform.rotation);
        chemicalCocktail.transform.parent = this.gameObject.transform;
        
    }

    private void ChemicalCocktailDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);

        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.gameObject.GetComponent<Stats>() == true)
            {
                hitCollider.GetComponent<DamageHandler>().ApplyDamage(new PoisonDamageData(hitCollider.GetComponent<DebuffHandler>().PoisonStack * poisonStackModifier), damageHandler);

            }
        }
    }
}