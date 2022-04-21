using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumNameFinder : MonoBehaviour
{
     public enum statusEffect 
     {
     Stun, 
     Root, 
     Silence, 
     Blindness, 
     Paralysis, 
     Crippled
     }

    // Start is called before the first frame update
    void Start()
    {
        string enumString = statusEffect.GetName(typeof(statusEffect),3);
        Debug.Log(enumString);
    }
}
