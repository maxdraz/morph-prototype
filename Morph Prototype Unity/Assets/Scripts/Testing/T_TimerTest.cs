using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class T_TimerTest : MonoBehaviour
{
    [SerializeField] private Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
            timer.RestartIfCompleted();
        
        timer.Update(Time.deltaTime);

        if (timer.JustStarted)
        {
            print("just started");
        }
        
        if (timer.JustCompleted)
        {
            print("just completed");
        }
    }
}
