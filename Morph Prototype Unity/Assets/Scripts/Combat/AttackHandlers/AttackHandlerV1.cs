using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandlerV1 : AttackHandler
{
    
    private List<LightAttack> lightAttacks;
    private List<HeavyAttack> heavyAttacks;
    private  List<Attack> attackQueue;
    private Attack currentAttack;
    private int currentAttackIndex;
    
    private bool attackInProgress;
    private float attackTimer;

    private void Awake()
    {
        lightAttacks = new List<LightAttack>();
        heavyAttacks = new List<HeavyAttack>();
        attackQueue = new List<Attack>();

        currentAttack = new LightAttack(1);
    }

    private void Update()
    {
        if (attackInProgress)
            attackTimer+= Time.deltaTime;
    }

    private IEnumerator ProcessAttackQueue()
    {
        while (attackQueue.Count > 0)
        {
            currentAttack = attackQueue[0];
           
            OnAttackStart();
            yield return new WaitForSeconds(currentAttack.duration);
           
            attackQueue.RemoveAt(0);
            OnAttackEnd();
        }
    }

    void OnAttackStart()
    {
        attackInProgress = true;
        attackTimer = 0;
    }

    void OnAttackEnd()
    {
        print("attack lasted for " + attackTimer);
        attackInProgress = false;
    }

    private bool CanQueue(in Attack incomingAttack)
    {
        if (QueueIsEmpty())
            return true;

        if (!WithinInputWindow())
            return false;

        if (ComboIsLegal(in incomingAttack))
        {
            return true;
        }
        
        return false;
    }

    private bool QueueIsEmpty()
    {
        return attackQueue.Count < 1;
    }

    private bool ComboIsLegal(in Attack incomingAttack)
    {
        if ((currentAttack is LightAttack && incomingAttack is HeavyAttack)
            || (currentAttack is HeavyAttack && incomingAttack is LightAttack))
        {
            //check if can transition to other type
            return currentAttack.canComboIntoOtherType;
        }
        //every other case is illegal
        return false;
    }

    private bool WithinInputWindow()
    {
        return attackTimer >= currentAttack.inputNextWindow;
    }

    public override void TryQueueAttack(bool isLight)
    {
        // if (!CanQueue())
        // {
        //     print("cant queue");
        //     return;
        // }
    
        //currentAttack = 
        //attackQueue.Add(new Attack(1)); add new attack to queue
        
        if (attackQueue.Count <= 1) //
        {
            StartCoroutine(ProcessAttackQueue());
        }
    }


    public override void TryQueueAttack(in Attack attack)
    {
        if (!CanQueue(in attack))
            return;
        
        QueueAttack(in attack);
    }

    void QueueAttack(in Attack attack)
    { 
        currentAttack = attack;
        attackQueue.Add(attack);
       
       if (attackQueue.Count <= 1) // restart coroutine if previous finished
       {
           StartCoroutine(ProcessAttackQueue());
       }
    }
}
