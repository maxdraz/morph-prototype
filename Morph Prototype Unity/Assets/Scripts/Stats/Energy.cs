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

    float energyRegenTimerDuration = 1f;
    public bool energyRegenOnCooldown;
    float energyRegen = 5;
    float globalEnergyRegenFactor = 100;

    Stamina stamina;
    Stats stats;

    float particleThreshold = 10;
    [SerializeField] private GameObject energyGainParticles;

    [SerializeField] private Image energyBar;
    private Coroutine hideEnergyBarAfterTime;

    // Start is called before the first frame update
    void Start()
    {
        stamina = GetComponent<Stamina>();
        stats = GetComponent<Stats>();
        baseMaxEnergy = stats ? stats.MaxEnergy : 100;

        Invoke("SetMaxEnergy",.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!energyRegenOnCooldown) 
        {
            EnergyRegen();
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
        currentEnergy = Mathf.Min(currentEnergy + amount, totalMaxEnergy);

        if (amount > particleThreshold) 
        {
            ObjectPooler.Instance.GetOrCreatePooledObject(energyGainParticles);
        }

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
        currentEnergy = Mathf.Max(0, currentEnergy - amount);
        T_UpdateEnergyBar();

        if (energyRegenOnCooldown)
        {
            StopCoroutine("RegenTimer");
            StartCoroutine("RegenTimer");
        }
        else 
        {
            StartCoroutine("RegenTimer");
        }
    }

    IEnumerator RegenTimer() 
    {
        energyRegenOnCooldown = true;

        yield return new WaitForSeconds(energyRegenTimerDuration);

        energyRegenOnCooldown = false;
    
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
