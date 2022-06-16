using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celerity : PassiveMorph
{
    [SerializeField] private float moveSpeedStatBonus = .2f;
    [SerializeField] private bool unlockGraceful;
    [SerializeField] private float mobilityStaminaCostReduction = 5;

    [SerializeField] private Movement movement;
    [SerializeField] private Stamina stamina;

    protected override void GetComponentReferences()
    {
        base.GetComponentReferences();
        
        stamina = GetComponent<Stamina>();
        movement = GetComponentInParent<Movement>();
    }

    protected override void OnEquip()
    {
        base.OnEquip();
        
        ChangeMoveSpeedStat(moveSpeedStatBonus);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();
        
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
}
