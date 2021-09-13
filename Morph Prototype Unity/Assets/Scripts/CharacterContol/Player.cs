using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    private void OnEnable()
    {
       
        StartCoroutine(SubscribeToEventsCoroutine());
    }

    private void OnDisable()
    {
        QuantumConsole.Instance.OnActivate -= OnConsoleActivate;
        QuantumConsole.Instance.OnDeactivate -= OnConsoleDeactivate;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SubscribeToEventsCoroutine()
    {
        yield return new WaitForEndOfFrame();
         QuantumConsole.Instance.OnActivate += OnConsoleActivate;
         QuantumConsole.Instance.OnDeactivate += OnConsoleDeactivate;
    }

    public void ReceiveInput(bool enable)
    {
        var movement = GetComponent<Movement>();
        if (movement == null) return;

        movement.enabled = enable;
    }

    private void OnConsoleActivate()
    {
        print("executed");
        ReceiveInput(false);
        CursorManager.SetCursorLockMode(CursorLockMode.None);
        Camera.main.GetComponent<ThirdPersonCamera>().enabled = false;
    }
    
    private void OnConsoleDeactivate()
    {
        ReceiveInput(true);
        CursorManager.SetCursorLockMode(CursorLockMode.Locked);
        Camera.main.GetComponent<ThirdPersonCamera>().enabled = true;
    }
}
