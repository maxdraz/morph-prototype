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
    private InputAction jumpAction;
    
    private InputAction limbLightAttackAction;
    private InputAction limbHeavyAttackAction;
    private InputAction tailLightAttackAction;
    private InputAction tailHeavyAttackAction;
    private InputAction mouthLightAttackAction;
    private InputAction mouthHeavyAttackAction;
    
    private InputAction Ability1Action;
    private InputAction Ability2Action;
    private InputAction Ability3Action;
    private InputAction Ability4Action;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CreatureVirtualController>();
        
        movementAction = playerInput.actions.FindAction("Movement");
        jumpAction = playerInput.actions.FindAction("Jump");
        
        limbLightAttackAction = playerInput.actions.FindAction("LimbLightAttack");
        limbHeavyAttackAction = playerInput.actions.FindAction("LimbHeavyAttack");
        
        tailLightAttackAction = playerInput.actions.FindAction("TailLightAttack");
        tailHeavyAttackAction = playerInput.actions.FindAction("TailHeavyAttack");
        
        mouthLightAttackAction = playerInput.actions.FindAction("MouthLightAttack");
        mouthHeavyAttackAction = playerInput.actions.FindAction("MouthHeavyAttack");
        
        Ability1Action = playerInput.actions.FindAction("Ability1");
        Ability2Action = playerInput.actions.FindAction("Ability2");
        Ability3Action = playerInput.actions.FindAction("Ability3");
        Ability4Action = playerInput.actions.FindAction("Ability4");
        
    }

    private void OnEnable()
    {
        jumpAction.performed += OnJumpInput;

        limbLightAttackAction.performed += OnLimbLightAttackPerformed;
        limbHeavyAttackAction.performed += OnLimbHeavyAttackPerformed;
        
        tailLightAttackAction.performed += OnTailLightAttackPerformed;
        tailHeavyAttackAction.performed += OnTailHeavyAttackPerformed;
        
        mouthLightAttackAction.performed += OnMouthLightAttackPerformed;
        mouthHeavyAttackAction.performed += OnMouthHeavyAttackPerformed;

        Ability1Action.performed += OnAbility1Performed;
        Ability2Action.performed += OnAbility2Performed;
        Ability3Action.performed += OnAbility3Performed;
        Ability4Action.performed += OnAbility4Performed;
    }

    private void OnDisable()
    {
        jumpAction.performed -= OnJumpInput;
        
        limbLightAttackAction.performed -= OnLimbLightAttackPerformed;
        limbHeavyAttackAction.performed -= OnLimbHeavyAttackPerformed;
        
        tailLightAttackAction.performed -= OnTailLightAttackPerformed;
        tailHeavyAttackAction.performed -= OnTailHeavyAttackPerformed;
        
        mouthLightAttackAction.performed -= OnMouthLightAttackPerformed;
        mouthHeavyAttackAction.performed -= OnMouthHeavyAttackPerformed;
        
        Ability1Action.performed -= OnAbility1Performed;
        Ability2Action.performed -= OnAbility2Performed;
        Ability3Action.performed -= OnAbility3Performed;
        Ability4Action.performed -= OnAbility4Performed;
    }

    private void Update()
    {
        var moveInput = movementAction.ReadValue<Vector2>();
       // var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        controller.Move(moveInput);
    }

    private void OnJumpInput(InputAction.CallbackContext ctx)
    {
        controller.Jump();
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

    private void OnAbility1Performed(InputAction.CallbackContext ctx)
    {
        controller.InvokeUseAbility1();
    }
    private void OnAbility2Performed(InputAction.CallbackContext ctx)
    {
        controller.InvokeUseAbility2();
    }
    private void OnAbility3Performed(InputAction.CallbackContext ctx)
    {
        controller.InvokeUseAbility3();
    }
    private void OnAbility4Performed(InputAction.CallbackContext ctx)
    {
        controller.InvokeUseAbility4();
    }
}
