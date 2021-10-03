using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class AttackStatusBar : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TextMeshProUGUI attackName;

    private AttackHandler playerAttackHandler;
    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>();
        //EnableSlider(false);

    }

    private void Start()
    {
        print("trying to get creature");
        playerAttackHandler = Player.Instance.GetActiveCreature().GetComponent<AttackHandler>();
        
        if (playerAttackHandler)
        {
            playerAttackHandler.AttackStarted += OnAttackStarted;
            playerAttackHandler.AttackInProgress += OnAttackInProgress;
            playerAttackHandler.AttackEnded += OnAttackEnded;
        }
    }

    private void OnDisable()
    {
        if (playerAttackHandler)
        {
            playerAttackHandler.AttackStarted -= OnAttackStarted;
            playerAttackHandler.AttackInProgress -= OnAttackInProgress;
            playerAttackHandler.AttackEnded -= OnAttackEnded;
        }
    }

    void OnAttackStarted(in Attack attack)
    {
        EnableSlider(true);
        DisplayStatusText(in attack);
    }

    void DisplayStatusText(in Attack attack)
    {
        if(!attackName) return;
        attackName.text = attack.name + " " + playerAttackHandler.ComboIndex;
    }
    
    
    void OnAttackInProgress(in Attack attack)
    {
        UpdateSlider(in attack);
    }

    void UpdateSlider(in Attack attack)
    {
        slider.value = attack.GetProgress();
    }
    
    void OnAttackEnded(in Attack attack)
    {
        if(playerAttackHandler.QueueIsEmpty())
            EnableSlider(false);
    }

    void EnableSlider(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
