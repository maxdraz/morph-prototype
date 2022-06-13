using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_InitializationTest : MonoBehaviour
{
    private void Awake()
    {
        print("awake");
    }
    
    private void OnEnable()
    {
        print("onenable");
    }
    private void Start()
    {
        print("start");
    }

    public void Initialize()
    {
        print("initialize");
    }
}
