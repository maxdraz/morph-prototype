using System;
using System.Collections.Generic;
using UnityEngine;

public class TestWeaponMorph : MonoBehaviour
{
    private List<LightAttack> lightAttacks;
    private List<HeavyAttack> HeavyAttacks;

    public delegate void TestDelegate(bool b); // This defines what type of method you're going to call.
    public TestDelegate m_methodToCall;

    private void Awake()
    {
        var contorller = GetComponent<CreatureVirtualController>();
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void SetInputs(TestDelegate func)
    {
        
    }
}
