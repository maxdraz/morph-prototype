using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatResources : MonoBehaviour
{
    Stats stats;
    private int staminaPointsMax;
    private int healthPointsMax;
    private int energyPointsMax;
    private float staminaPoints;
    private float healthPoints;
    private float energyPoints;

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

    public void UpdateCombatRescources(int newHp, int newEn, int newStam)
    {
        staminaPointsMax = newStam;
        healthPointsMax = newHp;
        energyPointsMax = newEn;
    }

    public float SpendStamina(float staminaCost) 
    {
        staminaPoints -= staminaCost;
        return staminaPoints;
    }

    public float SpendEnergy(float energyCost)
    {
        energyPoints -= energyCost;
        return energyPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaPoints < staminaPointsMax && staminaRegenRate > 0) 
        {
            staminaPoints = (staminaPoints + ((10 + (1 + (energyPoints/energyPointsMax))) * staminaRegenRate) * Time.deltaTime);
            
            if (staminaPoints > staminaPointsMax) 
            {
                staminaPoints = staminaPointsMax;
            }    
        }

        if (energyPoints < energyPointsMax && energyRegenRate > 0)
        {
            energyPoints = (energyPoints + (10 * energyRegenRate) * Time.deltaTime);

            if (energyPoints > energyPointsMax)
            {
                energyPoints = energyPointsMax;
            }
        }

        if (healthPoints < healthPointsMax && healthRegenRate > 0)
        {
            healthPoints = (healthPoints + (10 * healthRegenRate) * Time.deltaTime);

            if (healthPoints > healthPointsMax)
            {
                healthPoints = healthPointsMax;
            }
        }

        float healthBarSize = (1f + (healthPointsMax / 100));
        float energyBarSize = (1f + (energyPointsMax / 100));
        float staminaBarSize = (1f + (staminaPointsMax / 100));

        healthBar.rectTransform.localScale = new Vector3(healthBarSize, .2f, 1f); 
        energyBar.rectTransform.localScale = new Vector3(energyBarSize, .2f, 1f);
        staminaBar.rectTransform.localScale = new Vector3(staminaBarSize, .2f, 1f);

        //needs to display the current health,stamina, and energy as a function of the max size of the bar. As calculated above using the BarSize variables.
        currentHealthBar.localScale = new Vector3((1f + (healthPointsMax / healthPoints)), .2f, 1f);
        currentEnergyBar.localScale = new Vector3((1f + (energyPointsMax / energyPoints)), .2f, 1f);
        currentStaminaBar.localScale = new Vector3((1f + (staminaPointsMax / staminaPoints)), .2f, 1f);

    }
}
