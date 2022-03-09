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

    static Prerequisite[] StatPrerequisits = new Prerequisite[1]
{
        new Prerequisite("intimidation", intimidationPrerequisite),
};


    private void Start()
    {
        damageHandler = GetComponent<DamageHandler>();
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
        GameObject howl = ObjectPooler.Instance.GetOrCreatePooledObject(terrifyingHowlParticles);
        howl.transform.position = transform.position;
        howl.transform.parent = transform;

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
