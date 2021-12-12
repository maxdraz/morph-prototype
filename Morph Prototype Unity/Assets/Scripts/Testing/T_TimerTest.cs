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
      print(timer.CurrentTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.JustCompleted)
        {
            print("just completed");
        }
        
        if(Input.GetMouseButtonDown(0))
            timer.RestartIfCompleted();
        
        timer.Update(Time.deltaTime);
    }
}
