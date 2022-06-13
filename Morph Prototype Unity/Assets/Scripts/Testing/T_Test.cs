using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Test : MonoBehaviour
{
    public T_InitializationTest test;
    
    private void Awake()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var original = test;
            if (test) test = gameObject.CopyComponent(test);
            Destroy(original);
            test.Initialize();
        }
    }
}
