using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celerity : PassiveMorph
{
    //[SerializeField] private CelerityPrerequisiteData prerequisiteData;


    private DamageHandler damageHandler;
    [SerializeField] private float moveSpeedStatBonus = .2f;
    [SerializeField] private bool unlockGraceful;
    [SerializeField] private float mobilityStaminaCostReduction = 5;

    [SerializeField] private Movement movement;
    [SerializeField] private Stamina stamina;

    private void OnEnable()
    {
        stamina = GetComponent<Stamina>();
        movement = GetComponentInParent<Movement>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeMoveSpeedStat(moveSpeedStatBonus);
        
    }

    private void OnDisable()
    {
        stamina = GetComponent<Stamina>();
        movement = GetComponentInParent<Movement>();

        UnsubscribeFromEvents();
        ChangeMoveSpeedStat(-moveSpeedStatBonus);
    }

    public void UnlockSecondary(string name)
    {
        if (name == "Graceful")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockGraceful = true;
        }
    }

    // implement
    private void ChangeMoveSpeedStat(float amountToAdd)
    {
        movement.bonusPercentMoveSpeed += amountToAdd;
    }

    private void Update()
    {
        if (unlockGraceful) 
        {
            //if (player dodge detected) 
            //{
            //    refund a portion of the stamina cost
            //    stamina.RefundStamina(stamina cost of mobility tech used, mobilityStaminaCostReduction);
            //}
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
