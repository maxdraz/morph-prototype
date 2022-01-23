using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float staminaRegenBonus = .1f;
    [SerializeField] private float energyRegenBonus = .15f;
    [SerializeField] private bool unlockRecuperate = true;
    [SerializeField] private float recuperateTimerDuration = 1;
    [SerializeField] private Timer recuperateTimer;
    public bool recuperating;

    Stamina stamina;
    Energy energy;
    Rigidbody rb;

    private void OnEnable()
    {
        stamina = GetComponent<Stamina>();
        energy = GetComponent<Energy>();
        rb = transform.parent.gameObject.GetComponent<Rigidbody>();
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeStaminaEnergyRegenStat(staminaRegenBonus, energyRegenBonus);
    }

    private void OnDisable()
    {
        stamina = GetComponent<Stamina>();
        energy = GetComponent<Energy>();

        UnsubscribeFromEvents();
        ChangeStaminaEnergyRegenStat(-staminaRegenBonus, -energyRegenBonus);
    }

    // implement
    private void ChangeStaminaEnergyRegenStat(float staminaRegen, float energyRegen)
    {
        stamina.bonusStaminaRegen += staminaRegen;
        energy.bonusEnergyRegen += energyRegen;
    }

    private void Update()
    {
        if (unlockRecuperate) 
        {
            if (rb.velocity.magnitude == 0) 
            {
                recuperateTimer = new Timer(recuperateTimerDuration, false);
                recuperateTimer.Update(Time.deltaTime);

                if (recuperateTimer.JustCompleted)
                {
                    recuperating = true;
                    ChangeStaminaEnergyRegenStat(staminaRegenBonus, energyRegenBonus);
                }
            }

            else
            {
                if (recuperating == true) 
                {
                    recuperating = false;
                    ChangeStaminaEnergyRegenStat(-staminaRegenBonus, -energyRegenBonus);
                }
            }
        }
    }

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();
        if (damageHandler)
        {
            

        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            

        }

        damageHandler = null;
    }
}
