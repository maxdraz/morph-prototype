using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endurance : PassiveMorph
{
    private DamageHandler damageHandler;
    [SerializeField] private float staminaPercentageStatBonus = .20f;
    [SerializeField] private float staminaPercentageRegenBonus = .40f;
    [SerializeField] private bool unlockMoxie = true;

    Stamina stamina;

    private void OnEnable()
    {
        stamina = GetComponent<Stamina>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMaxStaminaStat(staminaPercentageStatBonus);

        if (unlockMoxie) 
        {
            stamina.bonusStaminaRegen += staminaPercentageRegenBonus;
        }
    }

    private void ChangeMaxStaminaStat(float amountToAdd)
    {
        stamina.maxStaminaBonus += amountToAdd;
        BroadcastMessage("SetMaxStamina");
    }

    private void OnDisable()
    {
        stamina = GetComponent<Stamina>();

        UnsubscribeFromEvents();
        ChangeMaxStaminaStat(-staminaPercentageStatBonus);

        if (unlockMoxie)
        {
            stamina.bonusStaminaRegen -= staminaPercentageRegenBonus;
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
