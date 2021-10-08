using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandlerV1 : AttackHandler
{
    private void Awake()
    {
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
       // print("attack started " + currentAttack.name);
        attackInProgress = true;
        attackTimer = 0;
    }

    void OnAttackEnd()
    {
      //  print("attack lasted for " + attackTimer);
        attackInProgress = false;

        if (QueueIsEmpty())
        {
            //combo over
            OnComboEnded();
        }
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
        var lastQueuedAttack = attackQueue[attackQueue.Count - 1];
        if (( lastQueuedAttack is LightAttack && incomingAttack is HeavyAttack)
            || (lastQueuedAttack is HeavyAttack && incomingAttack is LightAttack))
        {
            //check if can transition to other type
            return lastQueuedAttack.canComboIntoOtherType;
        }

        //every other case is illegal
        return true;
    }

    private bool WithinInputWindow()
    {
        return attackTimer >= currentAttack.inputNextWindow;
    }

    public override bool TryQueueAttack(in Attack attack)
    {
        if (!CanQueue(in attack))
            return false;
        
        QueueAttack(in attack);
        return true;
    }
    
    void QueueAttack(in Attack attack)
    { 
        //currentAttack = attack;
        attackQueue.Add(attack);
       
       if (attackQueue.Count <= 1) // restart coroutine if previous finished
       {
           StartCoroutine(ProcessAttackQueue());
       }
    }
}
