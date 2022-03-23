using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinisterWatcher : PassiveMorph
{
    static int stealthPrerequisit = 250;
    static int perceptionPrerequisit = 200;

    private DamageHandler damageHandler;
    [SerializeField] private int stealthStatBonus = 5;
    public DamageHandler targetOfInterest;
    [SerializeField] private float sinisterWatcherMaxRange;
    [SerializeField] private float sinisterWatcherBonusDamage;
    [SerializeField] private float sinisterWatcherMaxBonusDamage;


    [SerializeField] private bool unlockUnkownSource = true;

    Stats stats;

    //static Prerequisite[] StatPrerequisits;

    // Start is called before the first frame update
    private void OnEnable()
    {
        stats = GetComponent<Stats>();

        StartCoroutine(AssignDamageHandlerCoroutine());
        ChangeStealthStat(stealthStatBonus);
    }



    private void OnDisable()
    {

        UnsubscribeFromEvents();
        ChangeStealthStat(-stealthStatBonus);


    }

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sinisterWatcherMaxRange);
        float shortestDistance = 0;
        float distance;

        if (hitColliders.Length == 0)
        {
            return;
        }

        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.gameObject.GetComponent<Stats>() == true)
            {
                distance = (hitCollider.transform.position - transform.position).magnitude;

                if (shortestDistance == 0)
                {
                    sinisterWatcherBonusDamage = 0;
                    shortestDistance = distance;
                    targetOfInterest = hitCollider.GetComponent<DamageHandler>();
                }
                else 
                {

                    if (distance < shortestDistance) 
                    {
                        if (targetOfInterest == hitCollider.GetComponent<DamageHandler>())
                        {
                            sinisterWatcherBonusDamage += Time.deltaTime/10;

                            if (sinisterWatcherBonusDamage > sinisterWatcherMaxBonusDamage) 
                            {
                                sinisterWatcherBonusDamage = sinisterWatcherMaxBonusDamage;
                            }
                        }
                        else
                        {
                            sinisterWatcherBonusDamage = 0;
                            shortestDistance = distance;
                            targetOfInterest = hitCollider.GetComponent<DamageHandler>();
                        }
                    }
                }                
            }
        } 
    }

    private void ChangeStealthStat(int amountToAdd)
    {
        stats.FlatStatChange("stealth", amountToAdd);
    }

    private IEnumerator AssignDamageHandlerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        GetReferencesAndSubscribeToEvenets();
    }

    private void OnDamageAboutToBeDealt(ref IDamageType damageType)
    {
        //if (stealthAttack && damageTaker == targetOfInterest)
        {
            //physicalDamageToBeDealt *= 1 + sinisterWatcherBonusDamage;
        }

    }

    private void GetReferencesAndSubscribeToEvenets()
    {
        if (damageHandler) return;

        damageHandler = GetComponent<DamageHandler>();

        damageHandler.DamageAboutToBeDealt += OnDamageAboutToBeDealt;

        if (damageHandler)
        {
            if (unlockUnkownSource)
            {
            }
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (damageHandler)
        {
            damageHandler.DamageAboutToBeDealt -= OnDamageAboutToBeDealt;

            if (unlockUnkownSource)
            {
            }
        }

        damageHandler = null;
    }
}
