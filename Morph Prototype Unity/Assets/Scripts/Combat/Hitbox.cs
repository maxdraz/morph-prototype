using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private TagField tag;

    private Collider col;
    
    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<Collider>();
        
        //Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enable()
    {
        col.enabled = true;
    }

    public void Disable()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            print("Enemy hit");
        }
    }
}
