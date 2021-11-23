using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatResources : MonoBehaviour
{
    Stats stats;
    public int baseStaminaPoints;
    public int baseHealthPointsMax;
    public int baseEnergyPointsMax;
    public int staminaPointsMax;
    public int healthPointsMax;
    public int energyPointsMax;
    public float currentStaminaPoints;
    public float currentHealthPoints;
    public float currentEnergyPoints;

    private int energyRegenRate = 1;
    private int staminaRegenRate = 1;
    private int healthRegenRate = 0;

    Image healthBar;
    RectTransform currentHealthBar;

    Image staminaBar;
    RectTransform currentStaminaBar;

    Image energyBar;
    RectTransform currentEnergyBar;

    GameObject combatResourcesUI;

    // Start is called before the first frame update
    void Start()
    {
        combatResourcesUI = GameObject.Find("UI").transform.Find("Combat Resources").gameObject;

        healthBar = combatResourcesUI.transform.Find("Health").GetComponent<Image>();
        energyBar = combatResourcesUI.transform.Find("Energy").GetComponent<Image>();
        staminaBar = combatResourcesUI.transform.Find("Stamina").GetComponent<Image>();

        currentHealthBar = healthBar.transform.GetChild(0).GetComponent<RectTransform>();
        currentStaminaBar = staminaBar.transform.GetChild(0).GetComponent<RectTransform>();
        currentEnergyBar = energyBar.transform.GetChild(0).GetComponent<RectTransform>();


        stats = GetComponentInChildren<Stats>();
    }

    public void SetCombatRescources(int newHp, int newEn, int newStam)
    {
        staminaPointsMax = newStam;
        healthPointsMax = newHp;
        energyPointsMax = newEn;

        currentStaminaPoints = staminaPointsMax;
        currentHealthPoints = healthPointsMax;
        currentEnergyPoints = energyPointsMax;
    }

    public float SpendStamina(float staminaCost) 
    {
        currentStaminaPoints -= staminaCost;
        return currentStaminaPoints;
    }

    public float SpendEnergy(float energyCost)
    {
        currentEnergyPoints -= energyCost;
        return currentEnergyPoints;
    }

    public void DisableStaminaRegen()
    {
        staminaRegenRate = 0;
        InvokeRepeating("StaminaRegen",1.5f, 1.5f);
    }

    public void DelayStaminaRegen() 
    {
        staminaRegenRate = 0;
        CancelInvoke();
        Invoke("StaminaRegen", 1.5f);
    }

    private void RestartStaminaRegen() 
    {
        staminaRegenRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStaminaPoints < staminaPointsMax && staminaRegenRate > 0) 
        {
            currentStaminaPoints = (currentStaminaPoints + ((10 + (1 + (currentEnergyPoints/energyPointsMax))) * staminaRegenRate) * Time.deltaTime);
            
            if (currentStaminaPoints > staminaPointsMax) 
            {
                currentStaminaPoints = staminaPointsMax;
            }    
        }

        if (currentEnergyPoints < energyPointsMax && energyRegenRate > 0)
        {
            currentEnergyPoints = (currentEnergyPoints + (10 * energyRegenRate) * Time.deltaTime);

            if (currentEnergyPoints > energyPointsMax)
            {
                currentEnergyPoints = energyPointsMax;
            }
        }

        if (currentHealthPoints < healthPointsMax && healthRegenRate > 0)
        {
            currentHealthPoints = (currentHealthPoints + (10 * healthRegenRate) * Time.deltaTime);

            if (currentHealthPoints > healthPointsMax)
            {
                currentHealthPoints = healthPointsMax;
            }
        }

        float healthBarSize = (healthPointsMax / 500f);
        float energyBarSize = (energyPointsMax / 500f);
        float staminaBarSize = (staminaPointsMax / 500f);

        healthBar.rectTransform.localScale = new Vector3(healthBarSize + 2f, .2f, 1f); 
        energyBar.rectTransform.localScale = new Vector3(energyBarSize + 2f, .2f, 1f);
        staminaBar.rectTransform.localScale = new Vector3(staminaBarSize + 2f, .2f, 1f);

        //needs to display the current health,stamina, and energy as a function of the max size of the bar. As calculated above using the BarSize variables.
        currentHealthBar.localScale = new Vector3(((currentHealthPoints - (currentHealthPoints/50)) / healthPointsMax), .8f, 1f);
        currentEnergyBar.localScale = new Vector3(((currentEnergyPoints - (currentEnergyPoints/50)) / energyPointsMax), .8f, 1f);
        currentStaminaBar.localScale = new Vector3(((currentStaminaPoints - (currentStaminaPoints/50)) / staminaPointsMax), .8f, 1f);

    }
}
