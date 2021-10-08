using System.Collections.Generic;
using UnityEngine;

public class AttackHandlerV0 : AttackHandler
{

    [SerializeField] private List<LightAttack> lightAttacks;
    [SerializeField] private List<HeavyAttack> heavyAttacks;
    [SerializeField] private int comboIndex;
    public int ComboIndex => comboIndex;

    private Queue<Attack> attackQueue;
    private Attack currentAttack;

    public delegate void AttackStartedHandler(in Attack attack);
    public delegate void AttackInProgressHandler(in Attack attack);
    public delegate void AttackEndedHandler(in Attack attack);

    public AttackStartedHandler AttackStarted;
    public AttackInProgressHandler AttackInProgress;
    public AttackEndedHandler AttackEnded;
        

    // Start is called before the first frame update
    private void Awake()
    {
        lightAttacks = new List<LightAttack>();
        heavyAttacks = new List<HeavyAttack>();
        attackQueue = new Queue<Attack>();
    }

    // Update is called once per frame
    private void Update()
    {
       ProcessAttackQueue();
    }

    private void ProcessAttackQueue()
    {
        if (!QueueIsEmpty())
        {
            if (currentAttack != null)
            {
                if (currentAttack.completed)
                {
                    InitializeCurrentAttack();
                }
            }
            else
            {
                InitializeCurrentAttack();
            }

        }

        if (CurrentAttackCanExecute())
        {
            ExecuteAttack();
        }
    }

    private void ExecuteAttack()
    {
        currentAttack.Update();
        AttackInProgress?.Invoke(in currentAttack);
        
        if (currentAttack.completed)
        {
            AttackEnded?.Invoke(in currentAttack);
        }

        if (AttackCompletedAndQueueEmpty())
        {
            ResetCombo();
        }
    }

    private void ResetCombo()
    {
        comboIndex = 0;
    }

    private void InitializeCurrentAttack()
    {
        currentAttack = attackQueue.Dequeue();
        currentAttack.Start();
        AttackStarted?.Invoke(in currentAttack);
        comboIndex++;
    }

    public bool QueueIsEmpty()
    {
        return attackQueue.Count < 1;
    }

    private bool CurrentAttackCanExecute()
    {
        return currentAttack is { completed: false };
    }

    private bool AttackCompletedAndQueueEmpty()
    {
        return currentAttack.completed && attackQueue.Count < 1;
    }

    private void TryQueueLightAttack()
    {
        if (lightAttacks.Count > 0)
        {
            comboIndex %= lightAttacks.Count;
        }
        else
        {
            return;
        }

        if(!CanQueueAttack())
            return;

        if (currentAttack != null)
        {
            if (!currentAttack.completed &&!currentAttack.isLightAttack && !currentAttack.canComboIntoOtherType)
            {
                return;
            }

            if (!currentAttack.isLightAttack)
            {
                ResetCombo();
            }
        }

        attackQueue.Enqueue(lightAttacks[comboIndex]);
    }

    private bool CanQueueAttack()
    {
        if (currentAttack != null)
        {
            if (!currentAttack.completed && currentAttack.timeUntilComplete >= currentAttack.inputNextWindow)
            {
                return false;
            }
        }
        return true;
    }

    private void TryQueueHeavyAttack()
    {
        if (heavyAttacks.Count > 0)
        {
            comboIndex %= heavyAttacks.Count;
        }
        else
        {
            return;
        }

        if(!CanQueueAttack())
            return;

        if (currentAttack != null)
        {
            if (!currentAttack.completed &&currentAttack.isLightAttack && !currentAttack.canComboIntoOtherType)
            {
                return;
            }
            
            if (currentAttack.isLightAttack)
            {
                ResetCombo();
            }
        }

        attackQueue.Enqueue(heavyAttacks[comboIndex]);
    }
    
    public override void SetAttackData(List<LightAttack> lAttacks, List<HeavyAttack> hAttacks)
    {
        lightAttacks = lAttacks;
        heavyAttacks = hAttacks;
    }

    public override void TryQueueAttack(bool isLight)
    {
        
    }

    public override void TryQueueAttack(in Attack attack)
    {
        throw new System.NotImplementedException();
    }
}
