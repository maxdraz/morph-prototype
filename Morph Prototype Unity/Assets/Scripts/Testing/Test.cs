using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Test : MonoBehaviour
{
    public AssetReference data;
    [SerializeField] int currentLightAttackIndex;
    [SerializeField] List<TestAttack> lightAttacks;
    private TestAttack currentAttack;
    public Queue<TestAttack> attackQueue;
    
    public Action testAction;
    
    // Start is called before the first frame update
    void Start()
    {
        lightAttacks = new List<TestAttack>();
        lightAttacks.Add(new TestAttack(lightAttacks.Count, 3));
        lightAttacks.Add(new TestAttack(lightAttacks.Count,2));
        lightAttacks.Add(new TestAttack(lightAttacks.Count,1));
        lightAttacks.Add(new TestAttack(lightAttacks.Count,2));

        currentAttack = lightAttacks[0];
        attackQueue = new Queue<TestAttack>();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (LightAttackKeyPressed())
        {
            if(currentAttack.CanAcceptInput)
            {
                print("first attack of combo");
                currentLightAttackIndex %= lightAttacks.Count;
                attackQueue.Enqueue(lightAttacks[currentLightAttackIndex++ % lightAttacks.Count]);
            }
            else if (currentAttack.currentTime <= 0.5f) // queue next
            {
                print("queued next attack");
                currentLightAttackIndex %= lightAttacks.Count;
                attackQueue.Enqueue(lightAttacks[currentLightAttackIndex++]);
            }
        }
        
        if (attackQueue.Count > 0) // if there are queued combos
        {
            if (currentAttack.CanAcceptInput)
            {
                currentAttack = attackQueue.Dequeue();
                currentAttack.Start();
            }
        }

        if (currentAttack != null && !currentAttack.CanAcceptInput) // update current attack
        {
            currentAttack.Update();
        }
        else
        {
            currentLightAttackIndex = 0;
        }
    }

    bool LightAttackKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.Alpha0);
    }

    void Attack()
    {
        if (LightAttackKeyPressed())
        {
            if (currentAttack.CanAcceptInput)
            {
                currentAttack = lightAttacks[currentLightAttackIndex];
                currentAttack.Start();
                currentAttack.Ended += () =>
                {
                    print("combo reset");
                    currentLightAttackIndex = 0;
                };
            }
        }

        if (!currentAttack.CanAcceptInput)
        {
            currentAttack.Update();
        }
    }
    
    
}
