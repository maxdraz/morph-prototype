using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitriolicVitality : MonoBehaviour
{
    private DamageHandler damageHandler;
    [SerializeField] private float chemicalDamageStatBonus = 5;
    [SerializeField] private float healingPercentageBonus;
    Timer healingTimer;
    [SerializeField] private bool unlockVenomousVigor = true;
    [SerializeField] private bool unlockToxicFocus = true;

    private void Start()
    {
        healingTimer.Duration = 1f;
    }

    private void OnEnable()
    {
        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeChemicalDamageStat(chemicalDamageStatBonus);

    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
        ChangeChemicalDamageStat(-chemicalDamageStatBonus);
    }

    // implement
    private void ChangeChemicalDamageStat(float amountToAdd)
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (healingTimer.JustFinished) 
        {
            RemoveHealingBonus();
        }
    }

    void AddHealingBonus() 
    {
        damageHandler.Health.healingPercentageBonus += healingPercentageBonus;
    }

    void RemoveHealingBonus()
    {
        damageHandler.Health.healingPercentageBonus -= healingPercentageBonus;
    }



    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        if (damageTakenSummary.PoisonDamage > 0)
        {
            //This timer should restart every time a poison damage tick has been dealt to the target
            healingTimer.Restart(1, false);
            AddHealingBonus();

            if (unlockVenomousVigor) 
            {
                //This hp should be added when a target has had poison damage added to their stack
                damageHandler.Health.AddHP(damageTakenSummary.PoisonDamage / 2f);
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
            damageHandler.DamageHasBeenDealt += OnDamageHasBeenDealt;

            

            if (unlockToxicFocus)
            {

            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            if (unlockVenomousVigor)
            {

            }

            if (unlockToxicFocus)
            {

            }
        }

        damageHandler = null;
    }
}