using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGasCloud : MonoBehaviour
{
    DamageHandler damageHandler;
    float poisonDamageToDeal;
    float percentageMoveSpeedReduction;
    public float lifetime;
    public GameObject sourceCreature;
    Timer poisonTimer = new Timer();

    List<GameObject> enemiesInsideAOE = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        poisonTimer.Restart(1, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (poisonTimer.JustFinished) 
        {
            for (int i = 0; i < enemiesInsideAOE.Count; i++) 
            {
               enemiesInsideAOE[i].GetComponent<DamageHandler>().ApplyDamage(new PoisonDamageData(poisonDamageToDeal), damageHandler);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() == true && other.gameObject != sourceCreature)
        {
            enemiesInsideAOE.Add(other.gameObject);
            other.gameObject.GetComponent<Movement>().AdjustSpeedModifier(-percentageMoveSpeedReduction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() == true && other.gameObject != sourceCreature)
        {
            enemiesInsideAOE.Remove(other.gameObject);
            other.gameObject.GetComponent<Movement>().AdjustSpeedModifier(percentageMoveSpeedReduction);
        }
    }
}
