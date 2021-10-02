using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackHandler))]
public class BrutalSpursMorph : WeaponMorph
{
    private AttackHandler attackHandler;

    private void Awake()
    {
        attackHandler = GetComponent<AttackHandler>();
        attackHandler.Initialize(data.lightAttackData, data.heavyAttackData);
    }

    private void Update()
    {
        if (LightAttackKeyDown())
        {
            ExecuteLightAttack();
        }

        if (HeavyAttackKeyDown())
        {
            ExecuteHeavyAttack();
        }
    }

    void ExecuteLightAttack()
    {
        print("light attack");
    }

    void ExecuteHeavyAttack()
    {
        print("heavy attack");
    }

    bool LightAttackKeyDown()
    {
        return Input.GetMouseButtonDown(0);
    }

    bool HeavyAttackKeyDown()
    {
        return Input.GetMouseButtonDown(1);
    }
}
