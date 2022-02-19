using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActiveSlot : Slot
{
    private ActiveMorphHandler activeMorphHadler; // reference morph in slot

    [SerializeField] private TextMeshProUGUI cooldownText;
    
    protected override void Awake()
    {
        base.Awake();
        dropConditions.Add(new IsLimbWeaponMorphDropCondition());

        activeMorphHadler =
            PlayerCreatureCharacter.Instance.PartyManager.ActiveCreature.GetComponent<ActiveMorphHandler>();
        
        print(activeMorphHadler);
        
        DisplayCooldown();
    }

    private void OnEnable()
    {
        activeMorphHadler.ActiveMorphActivated += DisplayCooldown;
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        activeMorphHadler.ActiveMorphActivated -= DisplayCooldown;
    }

    // cooldown
    private void DisplayCooldown()
    {
      //  StartCoroutine(DisplayCooldownCoroutine(5));
    }

    IEnumerator DisplayCooldownCoroutine(ActiveMorph morph)
    {
        if (!cooldownText)
            yield break;
        
        cooldownText.gameObject.SetActive(true);
        
        while(morph.CurrentCooldownTime > 0)
        {
            cooldownText.text = morph.CurrentCooldownTime.ToString();
            yield return new WaitForEndOfFrame();
        }
        
        cooldownText.gameObject.SetActive(false);
    }
}
