using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_TimerTest : MonoBehaviour
{
    [SerializeField] private Timer abilityCooldown;
    [SerializeField] private GameObject objToSpawn;

    private Timer cooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        abilityCooldown ??= new Timer(2);

        var spawnedObj = ObjectPooler.Instance.GetOrCreatePooledObject(objToSpawn);
        
        ObjectPooler.Instance.Recycle(spawnedObj);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (abilityCooldown.JustStarted)
        {
            print("cooldown just started");
        }
        
        if (abilityCooldown.CountDown(Time.deltaTime))
        {
            print("cooldown running");
            
        }

        if (abilityCooldown.JustFinished)
        {
            print("cooldown just finished");
        }
        
        ValidateInput();
       
    }

    void ValidateInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            abilityCooldown.Resume();
            print("resumed");
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            abilityCooldown.Pause();
            print("paused");
        }
        
        if (Input.GetMouseButtonDown(2))
        {
            abilityCooldown.Restart();
            print("restarted");
        }
    }

    void Test()
    {
        
    }
}
