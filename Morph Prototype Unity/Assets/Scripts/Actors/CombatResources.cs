using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatResources : MonoBehaviour
{
    Stats stats;
    public int staminaPointsMax;
    public int healthPointsMax;
    public int energyPointsMax;
    public float currentStaminaPoints;
    public float currentHealthPoints;
    public float currentEnergyPoints;

    private int energyRegenRate = 1;
    private int staminaRegenRate = 1;
    private int healthRegenRate = 0;

    public int armourMax;
    public float currentArmour;
    public int armourSegments;

    Image healthBar;
    RectTransform currentHealthBar;

    RectTransform armourBar;
    RectTransform currentArmourBar;

    Image staminaBar;
    RectTransform currentStaminaBar;

    Image energyBar;
    RectTransform currentEnergyBar;

    GameObject combatResourcesUI;
    public GameObject armourSegment;

    float healthBarSize;
    float armourBarSize;
    float energyBarSize;
    float staminaBarSize;

    // Start is called before the first frame update
    void Start()
    {
        

        combatResourcesUI = GameObject.Find("UI").transform.Find("Combat Resources").gameObject;

        healthBar = combatResourcesUI.transform.Find("Health").GetComponent<Image>();
        armourBar = combatResourcesUI.transform.Find("Armour").GetComponent<RectTransform>();
        energyBar = combatResourcesUI.transform.Find("Energy").GetComponent<Image>();
        staminaBar = combatResourcesUI.transform.Find("Stamina").GetComponent<Image>();

        currentHealthBar = healthBar.transform.GetChild(0).GetComponent<RectTransform>();
        currentStaminaBar = staminaBar.transform.GetChild(0).GetComponent<RectTransform>();
        currentEnergyBar = energyBar.transform.GetChild(0).GetComponent<RectTransform>();

        healthBarSize = (healthPointsMax / 500f);
        energyBarSize = (energyPointsMax / 500f);
        staminaBarSize = (staminaPointsMax / 500f);

        int armourRemainder = armourMax % 100;
        armourMax -= armourRemainder;
        currentArmour = armourMax;
       //Debug.Log("armour remainder of " + armourRemainder);
        armourSegments = armourMax / 100;

        armourBarSize = (healthBarSize / armourSegments) * 2;

        for (int i = 1; i <= armourSegments; i++)
        {
            GameObject newArmourSegment = Instantiate(armourSegment, armourBar.transform);
            newArmourSegment.transform.localPosition = new Vector3(0, 0, 0);
            newArmourSegment.transform.localScale = new Vector3(armourBarSize, 1, 1);
        }

        //if (armourRemainder > 20) 
        //{
            //GameObject newArmourSegment = Instantiate(armourSegment, armourBar.transform);
            //newArmourSegment.transform.localPosition = new Vector3(0, 0, 0);
            //newArmourSegment.transform.localScale = new Vector3((healthBarSize / armourSegments) * (2 * (armourRemainder / 100)), 1, 1);
        //}

        stats = GetComponentInChildren<Stats>();
    }

    void SetCurrentArmourBar() 
    {
        for (int i = 1; i <= armourSegments; i++) 
        {
            currentArmourBar = armourBar.GetChild(i).GetComponent<RectTransform>();
        }
    }

    public float ReduceCurrentArmour(float armourToReduce) 
    {
        if (armourToReduce >= 100) 
        {
            armourToReduce = 100;

            if (armourToReduce > currentArmour % 100)
            {
                armourToReduce = currentArmour % 100;
            }
        }

        

        currentArmour -= armourToReduce;

        if (currentArmour % 100 == 00)
        {
            armourSegments--;
            currentArmourBar.gameObject.GetComponent<Image>().enabled = false;
            SetCurrentArmourBar();
        }
        else 
        {
            currentArmourBar.localScale = new Vector3((armourBarSize * (currentArmour % 100 / 100)), .8f, 1f);
        }

        return currentArmour;
    }

    public int SetArmourValue(int newArmour) 
    {
        armourMax += newArmour;

        return armourMax;
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
    }

    public void DelayStaminaRegen() 
    {
        staminaRegenRate = 0;
        CancelInvoke();
        Invoke("RestartStaminaRegen", 1.5f);
    }

    private void RestartStaminaRegen() 
    {
        staminaRegenRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left shift")) 
        {
            ReduceCurrentArmour(25);
        }

        if (currentStaminaPoints < staminaPointsMax && staminaRegenRate > 0) 
        {
            currentStaminaPoints = (currentStaminaPoints + ((staminaRegenRate * (1 + ((currentEnergyPoints/energyPointsMax) * 9))) * staminaRegenRate) * Time.deltaTime);
            
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
            currentHealthPoints = currentHealthPoints + (10 * healthRegenRate) * Time.deltaTime;

            if (currentHealthPoints > healthPointsMax)
            {
                currentHealthPoints = healthPointsMax;
            }
        }

        

        healthBar.rectTransform.localScale = new Vector3(healthBarSize + 2f, .2f, 1f);
        energyBar.rectTransform.localScale = new Vector3(energyBarSize + 2f, .2f, 1f);
        staminaBar.rectTransform.localScale = new Vector3(staminaBarSize + 2f, .2f, 1f);

        //needs to display the current health,stamina, and energy as a function of the max size of the bar. As calculated above using the BarSize variables.
        currentHealthBar.localScale = new Vector3((currentHealthPoints - (currentHealthPoints/50)) / healthPointsMax, .8f, 1f);
        currentEnergyBar.localScale = new Vector3((currentEnergyPoints - (currentEnergyPoints/50)) / energyPointsMax, .8f, 1f);
        currentStaminaBar.localScale = new Vector3((currentStaminaPoints - (currentStaminaPoints/50)) / staminaPointsMax, .8f, 1f);

    }
}
