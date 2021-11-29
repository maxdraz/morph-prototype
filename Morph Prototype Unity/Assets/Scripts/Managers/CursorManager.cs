using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private bool debug;

    public static Action OnCursorLocked;
    public static Action OnCursorUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        StartCoroutine(SubscribeToEventsCoroutine());
    }

    private void OnDisable()
    {
        if (QuantumConsole.Instance)
        {
            QuantumConsole.Instance.OnActivate -= OnConsoleActivate;
            QuantumConsole.Instance.OnDeactivate -= OnConsoleDeactivate;
        }
    }

    private void Update()
    {
        
    }

    private IEnumerator SubscribeToEventsCoroutine()
    {
        yield return new WaitForEndOfFrame();
        if (QuantumConsole.Instance)
        {
            QuantumConsole.Instance.OnActivate += OnConsoleActivate;
            QuantumConsole.Instance.OnDeactivate += OnConsoleDeactivate;
        }
    }

    private void OnConsoleActivate()
    {
        SetCursorLockMode(CursorLockMode.None);
        OnCursorLocked?.Invoke();
    }
    
    private void OnConsoleDeactivate()
    {
        SetCursorLockMode(CursorLockMode.Locked);
        OnCursorUnlocked?.Invoke();
    }

    public static void SetCursorLockMode(CursorLockMode mode)
    {
        Cursor.lockState = mode;
    }

    private void OnApplicationQuit()
    {
        SetCursorLockMode(CursorLockMode.None);
    }
}
