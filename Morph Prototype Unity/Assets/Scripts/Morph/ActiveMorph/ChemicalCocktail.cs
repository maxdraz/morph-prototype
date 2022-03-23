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

    //public Prerequisite[] StatPrerequisits;



    private void Start()
    {
        damageHandler = GetComponent<DamageHandler>();
        //WriteToPrerequisiteArray();
    }

   //void WriteToPrerequisiteArray()
   //{
   //    statPrerequisits = new Prerequisite[StatPrerequisits.Length];
   //
   //    for (int i = 0; i <= StatPrerequisits.Length - 1; i++)
   //    {
   //        statPrerequisits[i] = StatPrerequisits[i];
   //        Debug.Log(GetType().Name + " has a prerequisite " + statPrerequisits[i].stat + " of " + statPrerequisits[i].value);
   //    }
   //}

    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            SpawnChemicalCocktail();
            return true;
        }
        return false;
    }

    private void Update()
    {
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
