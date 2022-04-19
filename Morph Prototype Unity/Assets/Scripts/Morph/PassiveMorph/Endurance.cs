using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endurance : PassiveMorph
{
    //[SerializeField] private EndurancePrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private float staminaPercentageStatBonus = .20f;
    [SerializeField] private float staminaPercentageRegenBonus = .40f;
    [SerializeField] private bool unlockMoxie = true;

    Stamina stamina;

    private void Start()
    {
        stamina = GetComponent<Stamina>();
        ChangeMaxStaminaStat(staminaPercentageStatBonus);

        if (unlockMoxie)
        {
            stamina.bonusStaminaRegen += staminaPercentageRegenBonus;
        }
    }

    private void OnEnable()
    {

        StartCoroutine(AssignDamageHandlerCoroutine());
        
    }

    private void ChangeMaxStaminaStat(float amountToAdd)
    {
        BroadcastMessage("SetMaxStamina", amountToAdd);
    }

    private void OnDisable()
    {

        UnsubscribeFromEvents();

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
