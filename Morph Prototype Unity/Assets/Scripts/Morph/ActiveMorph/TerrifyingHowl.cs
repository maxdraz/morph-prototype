using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrifyingHowl : ActiveMorph
{
    static int intimidationPrerequisite = 150;
    [SerializeField] private GameObject terrifyingHowlParticles;
    [SerializeField] private float fortitudeDamage;
    [SerializeField] private float range;
    DamageHandler damageHandler;

    static Prerequisite[] BasePrerequisits = new Prerequisite[1]
{
        new Prerequisite("intimidation", intimidationPrerequisite),
};


    private void Start()
    {
        damageHandler = GetComponent<DamageHandler>();
    }
    public override bool ActivateIfConditionsMet()
    {
        if (base.ActivateIfConditionsMet())
        {
            if (GetComponent<Stealth>().stealthMode) 
            {
                Howl();
                return true;
            }
            return false;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(testInput))
        {
            Howl();
        }
    }

    private void Howl()
    {
        ObjectPooler.Instance.GetOrCreatePooledObject(terrifyingHowlParticles);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.gameObject.GetComponent<Stats>() == true)
            {
                //if (they had not detected you) 
                {
                    float distance = (transform.position - hitCollider.transform.position).magnitude;
                    float fortitudeDamageMultiplier = range - distance;
                    hitCollider.GetComponent<DamageHandler>().ApplyDamage(new FortitudeDamageData(fortitudeDamage * (1 + fortitudeDamageMultiplier)), damageHandler);
                }
            }
        }
    }

    private void OnValidate()
    {
    }

    private void OnDrawGizmos()
    {
    }
}
