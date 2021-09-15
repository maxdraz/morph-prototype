using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    
    
    [SerializeField] private Ability healAbility = new HealAbility(new Timer(2));
    [SerializeField] private Ability spellAbility = new SpellAbility(new Timer(2));
    [SerializeField] private Timer timer = new Timer(3);

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
                spellAbility.Use();
    }
}
