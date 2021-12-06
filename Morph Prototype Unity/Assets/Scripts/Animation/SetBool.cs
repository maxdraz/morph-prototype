using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBool : StateMachineBehaviour
{
    private enum TriggerEvent
    {
        OnEnter,
        OnUpdate,
        OnExit
    }

    [SerializeField] private TriggerEvent triggerEvent;
    [SerializeField] private string boolName;
    [SerializeField] private bool boolValue;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        
        if(triggerEvent == TriggerEvent.OnEnter)
            UpdateBoolValue(animator);
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        
        if(triggerEvent == TriggerEvent.OnExit)
            UpdateBoolValue(animator);
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        
        if(triggerEvent == TriggerEvent.OnUpdate)
            UpdateBoolValue(animator);
    }

    private void UpdateBoolValue(Animator animator)
    {
        if(boolName != "")
            animator.SetBool(boolName, boolValue);
    }
}
