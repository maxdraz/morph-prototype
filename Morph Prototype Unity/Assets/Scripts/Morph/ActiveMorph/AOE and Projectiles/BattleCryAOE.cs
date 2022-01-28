using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCryAOE : MonoBehaviour
{
    [SerializeField] private float range = 10;
    [SerializeField] private float volume = 100;

    public GameObject sourceCreature;


    // Start is called before the first frame update
    void Start()
    {
        BattleCry();
    }

    private void BattleCry()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.GetComponent<Perception>() == true && hitCollider.gameObject != sourceCreature)
            {
                float enemyPerception = hitCollider.GetComponent<Perception>().CurrentPerception;
                float distance = (hitCollider.transform.position - transform.position).magnitude;
                float perceptionTestValue = volume * (range - distance);

                if (perceptionTestValue > enemyPerception) 
                {
                    //The target has heard your battle cry and is now alert to your presence
                }
            }
        }
    }
}
