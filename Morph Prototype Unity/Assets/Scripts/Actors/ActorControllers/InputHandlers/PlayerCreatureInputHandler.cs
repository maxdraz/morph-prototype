using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCreatureInputHandler : CreatureInputHandler
{
    private PlayerInput playerInput;
    private CreatureVirtualController controller;
    
    private InputAction movementAction;
    private InputAction limbLightAttackAction;
    private InputAction limbHeavyAttackAction;
    private InputAction tailLightAttackAction;
    private InputAction tailHeavyAttackAction;
    private InputAction mouthLightAttackAction;
    private InputAction mouthHeavyAttackAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CreatureVirtualController>();
        
        movementAction = playerInput.actions.FindAction("Movement");
        limbLightAttackAction = playerInput.actions.FindAction("LimbLightAttack");
        limbHeavyAttackAction = playerInput.actions.FindAction("LimbHeavyAttack");
        
        tailLightAttackAction = playerInput.actions.FindAction("TailLightAttack");
        tailHeavyAttackAction = playerInput.actions.FindAction("TailHeavyAttack");
        
        mouthLightAttackAction = playerInput.actions.FindAction("MouthLightAttack");
        mouthHeavyAttackAction = playerInput.actions.FindAction("MouthHeavyAttack");
        
    }

    private void OnEnable()
    {

        limbLightAttackAction.performed += OnLimbLightAttackPerformed;
        limbHeavyAttackAction.performed += OnLimbHeavyAttackPerformed;
        
        tailLightAttackAction.performed += OnTailLightAttackPerformed;
        tailHeavyAttackAction.performed += OnTailHeavyAttackPerformed;
        
        mouthLightAttackAction.performed += OnMouthLightAttackPerformed;
        mouthHeavyAttackAction.performed += OnMouthHeavyAttackPerformed;
    }

    private void OnDisable()
    {
        limbLightAttackAction.performed -= OnLimbLightAttackPerformed;
        limbHeavyAttackAction.performed -= OnLimbHeavyAttackPerformed;
        
        tailLightAttackAction.performed -= OnTailLightAttackPerformed;
        tailHeavyAttackAction.performed -= OnTailHeavyAttackPerformed;
        
        mouthLightAttackAction.performed -= OnMouthLightAttackPerformed;
        mouthHeavyAttackAction.performed -= OnMouthHeavyAttackPerformed;
    }

    private void Update()
    {
        var moveInput = movementAction.ReadValue<Vector2>();
        controller.Move(moveInput);
    }

    private void OnMovementPerformed(InputAction.CallbackContext ctx)
    {
        controller.Move(ctx.ReadValue<Vector2>());
    }

    private void OnLimbLightAttackPerformed(InputAction.CallbackContext ctx)
    {
        controller.InvokeLimbLightAttack();
    }
    
    private void OnLimbHeavyAttackPerformed(InputAction.CallbackContext ctx)
    {
        controller.InvokeLimbHeavyAttack();
    }
    
    private void OnTailLightAttackPerformed(InputAction.CallbackContext ctx)
    {
        controller.InvokeTailLightAttack();
    }
    
    private void OnTailHeavyAttackPerformed(InputAction.CallbackContext ctx)
    {
        controller.InvokeTailHeavyAttack();
    }
    
    private void OnMouthLightAttackPerformed(InputAction.CallbackContext ctx)
    {
        controller.InvokeMouthLightAttack();
    }
    
    private void OnMouthHeavyAttackPerformed(InputAction.CallbackContext ctx)
    {
        controller.InvokeMouthHeavyAttack();
    }
}
