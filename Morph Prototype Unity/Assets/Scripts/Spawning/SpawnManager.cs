using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Mono.CSharp;
using QFSW.QC;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    private static SpawnPoint currentSpawnPoint;
    private static SpawnPoint entrySpawnPoint;
    [SerializeField] private List<SpawnPoint> spawnPoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        var player = GameObject.FindWithTag("Player");
        
        if (currentSpawnPoint == null)
        {
            if (spawnPoints.Count < 1)
            {
                //no spawn points
                Debug.LogWarning(this.name +": No spawn points in scene");
                player.transform.position = new Vector3(0, 2, 0);
            }
            else
            {
                Respawn(player.transform, 0);
            }
        }
        else
        {
            Respawn(player.transform);
        }
       
    }
    
    public static void AddSpawnPoint(SpawnPoint sp)
    {
        if (Instance == null)
        {
            var sManager = new GameObject("SpawnManager");
            sManager.AddComponent(typeof(SpawnManager));
            Instance = sManager.GetComponent<SpawnManager>();
            Instance.spawnPoints = new List<SpawnPoint>();
            Instance.spawnPoints.Add(sp);
        }
        else
        {
            Instance.spawnPoints.Add(sp);
        }
    }

    [Command("respawn")]
    public static void Respawn()
    {
        var player = GameObject.FindWithTag("Player").transform; 
        player.position = currentSpawnPoint.transform.position;
        player.rotation = currentSpawnPoint.transform.rotation;
    }
    
    [Command("respawn-at")]
    public static void Respawn(int spawnPointIndex)
    {
        var player = GameObject.FindWithTag("Player").transform; 
        Respawn(player, spawnPointIndex);
    }
    
    public static void Respawn(Transform me, int spawnPointIndex)
    {
        spawnPointIndex = Mathf.Clamp(spawnPointIndex, 0, Instance.spawnPoints.Count);
        me.transform.position = Instance.spawnPoints[spawnPointIndex].transform.position;
        me.transform.rotation = Instance.spawnPoints[spawnPointIndex].transform.rotation;
    }
    
    [Command("respawn-entry")]
    public static void RespawnAtEntry()
    {
        print(entrySpawnPoint.name);
        var player = GameObject.FindWithTag("Player").transform;
        Respawn(player, entrySpawnPoint);
    }
    
    public static void Respawn(Transform me)
    {
        me.position = currentSpawnPoint.transform.position;
        me.rotation = currentSpawnPoint.transform.rotation;
    }
    
    public static void Respawn(Transform me, SpawnPoint sp)
    {
        me.position = sp.transform.position;
        me.rotation = sp.transform.rotation;
    }


    public static void SetCurrentSpawnPoint(SpawnPoint sp)
    {
        currentSpawnPoint = sp;
    }

    public static void SetEntrySpawnPoint(SpawnPoint sp)
    {
        entrySpawnPoint = sp;
        SetCurrentSpawnPoint(entrySpawnPoint);
    }
}
