using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private bool debug;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void SetCursorLockMode(CursorLockMode mode)
    {
        Cursor.lockState = mode;
    }
}
