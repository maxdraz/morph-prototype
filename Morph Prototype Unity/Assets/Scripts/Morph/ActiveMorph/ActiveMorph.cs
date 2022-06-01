using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActiveMorphHandler))]
public class ActiveMorph : Morph
{

    [SerializeField] protected float staminaCost;
    [SerializeField] protected float energyCost;

    [SerializeField] protected KeyCode testInput;

    [SerializeField] protected Timer castTimer;
    [SerializeField] protected Timer cooldown;

    private Movement movement;  // TODO - make movement listen to attack handler to change
    private CreatureVirtualController controller;
    private Stamina stamina;
    private Energy energy;

    public RaycastHit hit;
    private Vector3 raycastToGroundTarget;

    public float CurrentCooldownTime => cooldown.CurrentTime;

    public void RaycastToGround() 
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        

        if (Physics.Raycast(ray, out hit))
        {
            
            raycastToGroundTarget = hit.point;
            return;
        }

        else
        {
            raycastToGroundTarget = new Vector3(0, 0, 0);
            return;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        stamina = GetComponent<Stamina>();
        energy = GetComponent<Energy>();
        movement = GetComponentInParent<Movement>();
        controller = GetComponentInParent<CreatureVirtualController>();
    }

    protected override void Update()
    {
        base.Update();
        
        castTimer.Update(Time.deltaTime);
        cooldown.Update(Time.deltaTime);

    }

    public void SpendStamina(float amount) 
    {
        stamina.SubtractStamina(amount);
        Debug.Log("spending " + staminaCost +  " stamina");
    }

    public void SpendEnergy(float amount)
    {
        energy.SubtractEnergy(amount);
        Debug.Log("spending " + energyCost + " energy");
    }

    public bool CheckEnergy(float amount)
    {
        if (amount >= energy.currentEnergy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckStamina(float amount)
    {
        if (amount >= stamina.currentStamina)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool ActivateIfConditionsMet()
    {
        bool shouldActivate = cooldown.RestartIfCompleted();

        //need to check if the stamina and energy scripts have the resources to use the active
        if (shouldActivate && CheckEnergy(energyCost) && CheckStamina(staminaCost))
        {
            controller.CharacterRotator.StartRotating(
                CharacterRotationMode.CameraForward, 
                CharacterRotationMode.Velocity, 
                new Timer(1f));
            
            //Spend the energy and stamina
            SpendEnergy(energyCost);
            SpendStamina(staminaCost);
        }

        return shouldActivate;
    }
}
