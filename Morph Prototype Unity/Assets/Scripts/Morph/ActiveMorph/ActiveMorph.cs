using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActiveMorphHandler))]
public class ActiveMorph : MonoBehaviour
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

    public float CurrentCooldownTime => cooldown.CurrentTime;


    [SerializeField] public struct Prerequisite
    {
        string stat;
        int value;

        public Prerequisite(string a, int b)
        {
            stat = a;
            value = b;

            
        }
    }

    private void Awake()
    {
        stamina = GetComponent<Stamina>();
        energy = GetComponent<Energy>();
        movement = GetComponentInParent<Movement>();
        controller = GetComponentInParent<CreatureVirtualController>();
    }

    private void Update()
    {
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
        if (amount > energy.currentEnergy)
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
        if (amount > stamina.currentStamina)
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
