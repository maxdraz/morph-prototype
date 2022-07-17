using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stamina : MonoBehaviour
{
    [SerializeField]private float baseMaxStamina;
    public float maxStaminaBonus;
    public float bonusStaminaRegen;
    [SerializeField] private float totalMaxStamina;
    public float currentStamina;
    public float energyAsPercentage;
    public float CurrentStaminaAsPercentage => currentStamina / totalMaxStamina;

    float staminaRegenTimerDuration = 1f;
    public bool staminaRegenOnCooldown;
    public bool grounded;
    Vector3 groundCheckOffset = new Vector3(0, -.5f, 0);
    float staminaRegen = 5;
    float globalStaminaRegenFactor = 50;

    Stats stats;

    float particleThreshold = 10;
    [SerializeField] private GameObject staminaGainParticles;

    [SerializeField] private Image staminaBar;
    private Coroutine hideStaminaBarAfterTime;

    // Start is called before the first frame update
    void Start()
    { 
        staminaRegenOnCooldown = false;
        stats = GetComponent<Stats>();
        baseMaxStamina = stats ? stats.MaxStamina : 100;
        totalMaxStamina = baseMaxStamina * (1 + stats.FortitudeMaxStaminaModifier);

        currentStamina = totalMaxStamina;

    }

    // Update is called once per frame
    void Update()
    {
        FindCurrentStaminaAsPercentage();

        if (currentStamina < totalMaxStamina)
        {
            if (!staminaRegenOnCooldown && grounded)
            {
                StaminaRegen();
            }
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position + groundCheckOffset, .2f);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform == gameObject.transform)
            {
                if (hitColliders.Length == 1)
                {
                    grounded = false;
                }

                return;
            }

            if (hitCollider.tag == "Ground")
            {
                grounded = true;
            }
        }
    }

    void StaminaRegen() 
    {
        float staminaToAdd = staminaRegen * (1 + bonusStaminaRegen + energyAsPercentage) / globalStaminaRegenFactor;
        //T_UpdateStaminaBar();
        AddStamina(staminaToAdd);
    }
  
    public void SetMaxStamina(float maxStaminaBonusToAdd)
    {
        maxStaminaBonus += maxStaminaBonusToAdd;
        totalMaxStamina = baseMaxStamina * (1 + maxStaminaBonus);
        

        T_SetUpStaminabar();
    }

    public void AddStamina(float amount)
    {
        currentStamina = Mathf.Min(currentStamina + amount, totalMaxStamina);

        if (amount > particleThreshold)
        {
            GameObject particles = ObjectPooler.Instance.GetOrCreatePooledObject(staminaGainParticles);
            particles.transform.position = transform.position;
            particles.transform.parent = transform;
        }

        T_UpdateStaminaBar();
    }

    

    public float FindCurrentStaminaAsPercentage()
    {
        float staminaAsPercetage = currentStamina / totalMaxStamina;
   
   
        return staminaAsPercetage;
    }

    

    public void SubtractStamina(float amount)
    {
        currentStamina = Mathf.Max(0, currentStamina - amount);
        T_UpdateStaminaBar();

        
        StopCoroutine("RegenTimer");
        if (grounded) 
        StartCoroutine("RegenTimer");
        
    }

    public IEnumerator RegenTimer()
    {
        Debug.Log("StaminaRegenTimer has started");

        staminaRegenOnCooldown = true;

        yield return new WaitForSeconds(staminaRegenTimerDuration);

        staminaRegenOnCooldown = false;

        Debug.Log("StaminaRegenTimer has finished");

    }


    private void T_SetUpStaminabar()
    {
        //staminaBar = GameObject.Find("UI").transform.Find("Gameplay").transform.Find("StatusBar").transform.Find("StaminaBar").GetComponent<Image>();
        staminaBar.gameObject.SetActive(false);
    }

    private void T_UpdateStaminaBar()
    {
        staminaBar.GetComponent<Image>().color = new Color(255, 255, 0, 255);
        staminaBar.fillAmount = currentStamina / totalMaxStamina;
        if (hideStaminaBarAfterTime != null) StopCoroutine(hideStaminaBarAfterTime);
        hideStaminaBarAfterTime = StartCoroutine(HideStaminaBarAfterTimeCoroutine(2));
    }

    private IEnumerator HideStaminaBarAfterTimeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        staminaBar.GetComponent<Image>().color = new Color(255, 255, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.tag == "Ground")
        {
            grounded = true;
            StartCoroutine("RegenTimer");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform.tag == "Ground")
        {
            grounded = false;
            StopCoroutine("RegenTimer");
        }
    }
}
