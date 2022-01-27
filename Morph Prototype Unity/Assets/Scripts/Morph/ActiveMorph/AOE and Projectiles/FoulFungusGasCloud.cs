using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoulFungusGasCloud : MonoBehaviour
{
    DamageHandler damageHandler;
    [SerializeField] private float poisonDamageDoT;
    [SerializeField] private float accuracyReduction;
    [SerializeField] private float stealthBonus;

    SphereCollider collider;
    GameObject sourceCreature;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() == true && other.gameObject != sourceCreature)
        {
            //lower the subjects accuracy by accuracyReduction
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() == true && other.gameObject != sourceCreature)
        {
            //return the accuracyReduction back to the subject
        }
    }
}
