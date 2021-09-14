using System.Collections;
using System.Collections.Generic;
using Mono.CSharp;
using UnityEngine;

public abstract class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    virtual protected void Awake() 
    {
        SpawnManager.AddSpawnPoint(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
