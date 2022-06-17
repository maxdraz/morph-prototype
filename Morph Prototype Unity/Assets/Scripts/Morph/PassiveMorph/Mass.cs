using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : PassiveMorph
{
    [SerializeField] private float percentHealthBonus = .20f;
    [SerializeField] private bool unlockRestoration;

    [SerializeField] private float restorationPercentHealthRegen = .02f;

    [SerializeField] private Health health;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        health = GetComponent<Health>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ChangeMaxHealthStat(percentHealthBonus);
        if (unlockRestoration) 
        {
            StartCoroutine("Restoration");
        }
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
        ChangeMaxHealthStat(-percentHealthBonus);
        if (unlockRestoration)
        {
            StopCoroutine("Restoration");
        }
    }

    public void UnlockSecondary(string name)
    {
        if (name == "Restoration")
        {
            Debug.Log(GetType().Name + "Unlocking " + name);
            unlockRestoration = true;
        }
    }

    IEnumerator Restoration() 
    {
        yield return new WaitForSeconds(1);

        transform.parent.gameObject.BroadcastMessage("AddPercentHP", restorationPercentHealthRegen);

        StartCoroutine("Restoration");

        yield return null;
    }

    // implement
    private void ChangeMaxHealthStat(float amountToAdd)
    {
        health.maxHealthBonus += amountToAdd;
    }
}
