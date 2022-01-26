using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomGlands : MonoBehaviour
{
    private DamageHandler damageHandler;
    [SerializeField] private float bonusPoisonDamage = 200;
    [SerializeField] private bool unlockAtrophy;
    [SerializeField] private float atrophyAttackSpeedReduction = .2f;

    [SerializeField] private bool unlockHaze;
    [SerializeField] private float hazeCooldownIncrease = .3f;

    [SerializeField] private bool unlockAcrid;
    [SerializeField] private float acridPoisonResistReduction = 10;

    [SerializeField] private bool unlockAnaesthetic;
    [SerializeField] private float anaestheticFortitudeDamage = 30;

    //NOTE only one of these special venoms can be active at a time, the bonusPoisonDamage is always dealt no matter which special venom is active

    [SerializeField] private float venomDuration = 4;

    private void OnEnable()
    {

        StartCoroutine(AssignDamageHandlerCoroutine());
    }

    private void OnDisable()
    {

        UnsubscribeFromEvents();
    }



    private void OnDamageHasBeenDealt(in DamageTakenSummary damageTakenSummary)
    {
        //if (damageTakenSummary.isHeavyAttack && damageTakenSummary.isBiteAttack)
        {
            damageTakenSummary.DamageTaker.ApplyDamage(new PoisonDamageData(bonusPoisonDamage), damageHandler);

            if (unlockAtrophy)
            {
                //Atrophy slows the targets attacks speed by atrophyAttackSpeedReduction for venomDuration
                //damageTakenSummary.DamageTaker.
            }

            if (unlockHaze)
            {
                //Haze increases the targets cooldowns by hazeCooldownIncrease for venomDuration
                //damageTakenSummary.DamageTaker.
            }

            if (unlockAcrid)
            {
                //Acrid reduces the targets poison resist by acridPoisonResistReduction for venomDuration
                //damageTakenSummary.DamageTaker.
            }

            if (unlockAnaesthetic)
            {
                //Anaesthetic deals anaestheticFortitudeDamage fortitude damage and stuns for venomDuration if successful
                //damageTakenSummary.DamageTaker.
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
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {

            damageHandler.DamageHasBeenDealt -= OnDamageHasBeenDealt;

        }

        damageHandler = null;
    }
}
