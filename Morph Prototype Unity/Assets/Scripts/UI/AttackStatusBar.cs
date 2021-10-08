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

    private AttackHandlerV0 playerAttackHandlerV0;
    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>();
        //EnableSlider(false);

    }

    private void Start()
    {
        print("trying to get creature");
        playerAttackHandlerV0 = Player.Instance.GetActiveCreature().GetComponent<AttackHandlerV0>();
        
        if (playerAttackHandlerV0)
        {
            playerAttackHandlerV0.AttackStarted += OnAttackStarted;
            playerAttackHandlerV0.AttackInProgress += OnAttackInProgress;
            playerAttackHandlerV0.AttackEnded += OnAttackEnded;
        }
    }

    private void OnDisable()
    {
        if (playerAttackHandlerV0)
        {
            playerAttackHandlerV0.AttackStarted -= OnAttackStarted;
            playerAttackHandlerV0.AttackInProgress -= OnAttackInProgress;
            playerAttackHandlerV0.AttackEnded -= OnAttackEnded;
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
        attackName.text = attack.name + " " + playerAttackHandlerV0.ComboIndex;
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
        if(playerAttackHandlerV0.QueueIsEmpty())
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
