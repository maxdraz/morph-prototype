using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloat : StateMachineBehaviour
{
    private enum TriggerEvent
    {
        OnEnter,
        OnUpdate,
        OnExit
    }

    [SerializeField] private TriggerEvent triggerEvent;
    [SerializeField] private string floatName;
    [SerializeField] private float newValue;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        
        if(triggerEvent == TriggerEvent.OnEnter)
            UpdateFloatValue(animator);
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        
        if(triggerEvent == TriggerEvent.OnExit)
            UpdateFloatValue(animator);
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        
        if(triggerEvent == TriggerEvent.OnUpdate)
            UpdateFloatValue(animator);
    }

    private void UpdateFloatValue(Animator animator)
    {
        if(floatName != "")
            animator.SetFloat(floatName, newValue);
    }
}
