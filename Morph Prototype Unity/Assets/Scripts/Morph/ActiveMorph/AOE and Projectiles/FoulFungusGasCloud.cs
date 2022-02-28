using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoulFungusGasCloud : MonoBehaviour
{
    DamageHandler damageHandler;
    [SerializeField] private float accuracyReduction;
    [SerializeField] private float stealthBonus;

    GameObject sourceCreature;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() == true)
        {
            if (other.gameObject != sourceCreature)
            {
                {
                    //lower the subjects accuracy by accuracyReduction
                }
            }
            else 
            {
                //add to the players stealth stat...make sure the player enters the trigger when is it spawned. If this does not happen add stealthBonus with a Start function
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() == true)
        {
            if (other.gameObject != sourceCreature)
            {
                {
                    //return the subjects accuracy to normal
                }
            }
            else
            {
                //return the players stealth stat to normal
            }
        }
    }
}
