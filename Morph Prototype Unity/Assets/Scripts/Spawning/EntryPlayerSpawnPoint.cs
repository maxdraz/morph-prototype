using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPlayerSpawnPoint : PlayerSpawnPoint
{
    public static EntryPlayerSpawnPoint Instance;
    // Start is called before the first frame update
    override protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning(this.name + ": More than one entry player spawn point in scene");
            Destroy(this);
        }
        
        SpawnManager.SetEntrySpawnPoint(this);
        
        base.Awake();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
