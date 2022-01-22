using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Energy : MonoBehaviour
{
    [SerializeField] private float baseMaxEnergy;
    public float bonusMaxEnergy;
    public float bonusEnergyRegen;
    [SerializeField] private float totalMaxEnergy;
    public float currentEnergy;
    float energyAsPercentage;

    //this timer starts every time energy is spent, during this timer energy wont regenerate
    [SerializeField] private Timer energyRegenTimer;
    float energyRegenTimerDuration = 1f;
    bool energyRegenOnCooldown;
    float energyRegen = 5;
    float globalEnergyRegenFactor = 100;

    Stamina stamina;
    Stats stats;

    [SerializeField] private Image energyBar;
    private Coroutine hideEnergyBarAfterTime;

    // Start is called before the first frame update
    void Start()
    {
        stamina = GetComponent<Stamina>();
        stats = GetComponent<Stats>();
        baseMaxEnergy = stats ? stats.MaxEnergy : 100;
        energyRegenTimer = new Timer(energyRegenTimerDuration, false);

        Invoke("SetMaxEnergy",.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!energyRegenOnCooldown)
        {
            if (currentEnergy < totalMaxEnergy) 
            {
                EnergyRegen();
            }
        }

        else
        {
            energyRegenTimer.Update(Time.deltaTime);

            if (energyRegenTimer.JustCompleted)
            {
                energyRegenOnCooldown = false;
            }
        }

        EnergyAsPercentage();
        stamina.energyAsPercentage = energyAsPercentage;
    }

    void EnergyRegen()
    {
        float energyToAdd = (energyRegen * (1 + bonusEnergyRegen)) / globalEnergyRegenFactor;
        AddEnergy(energyToAdd);
    }

    public void SetMaxEnergy()
    {
        totalMaxEnergy = baseMaxEnergy * (1 + bonusMaxEnergy);
        currentEnergy = totalMaxEnergy;

        T_SetUpEnergybar();
    }

    public void AddEnergy(float amount)
    {
        totalMaxEnergy = Mathf.Min(currentEnergy + amount, totalMaxEnergy);

        T_UpdateEnergyBar();
    }

    public void RefundEnergy(float amountSpent, float amountToRefund)
    {
        float energyToRefund = amountSpent * amountToRefund;


        AddEnergy(energyToRefund);

    }

    public float EnergyAsPercentage() 
    {
        energyAsPercentage = currentEnergy / totalMaxEnergy;
        return energyAsPercentage;
    }

    public void SubtractEnergy(float amount)
    {


        totalMaxEnergy = Mathf.Max(0, totalMaxEnergy - amount);
        energyRegenOnCooldown = true;
        T_UpdateEnergyBar();

        //if timer is still counting down, spend the energy and restart the timer from the beginning 
        if (energyRegenTimer.CurrentTime < energyRegenTimer.Duration)
        {
            energyRegenTimer = new Timer(energyRegenTimerDuration, false);
        }
    }

    private void T_SetUpEnergybar()
    {
        energyBar = GameObject.Find("UI").transform.Find("Gameplay").transform.Find("StatusBar").transform.Find("EnergyBar").GetComponent<Image>();
        energyBar.gameObject.SetActive(false);
    }

    private void T_UpdateEnergyBar()
    {
        // health bar
        energyBar.gameObject.SetActive(true);
        energyBar.fillAmount = currentEnergy / totalMaxEnergy;
        if (hideEnergyBarAfterTime != null) StopCoroutine(hideEnergyBarAfterTime);
        hideEnergyBarAfterTime = StartCoroutine(HideEnergyBarAfterTimeCoroutine(2));
    }

    private IEnumerator HideEnergyBarAfterTimeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        energyBar.gameObject.SetActive(false);
    }
}
