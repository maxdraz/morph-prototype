using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Armor : MonoBehaviour
{
    [SerializeField] private float baseMaxArmor;
    public float bonusFlatMaxArmor;
    public float bonusPercentMaxArmor;
    [SerializeField] private float maxArmor;


    public float currentArmor;
    public bool HasArmor => currentArmor > 0;

    public int armorSegments;
    public GameObject armorSegment;
    RectTransform currentArmorBar;
    float armorBarSize;

    [SerializeField] private Transform armorBar;
    private Coroutine hideArmorBarAfterTime;
    Image currentArmorBarImage;

    Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();

        T_SetUpArmorbar();
        Invoke ("SetMaxArmor",.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetMaxArmor()
    {
        baseMaxArmor = stats ? stats.MaxArmour : 100; 
        maxArmor = (baseMaxArmor * (1+bonusPercentMaxArmor))+ bonusFlatMaxArmor;

        float armourRemainder = maxArmor % 100;
        maxArmor -= armourRemainder;
        currentArmor = maxArmor;

        armorSegments = (int)maxArmor / 100;

        //float armourBarSize = (healthBarSize / armourSegments) * 2;

        

        T_SetUpArmorbar();
    }

    private void T_SetUpArmorbar()
    {
        armorBar = transform.Find("HealthBarCanvas").Find("ArmourBar");
        armorBar.gameObject.SetActive(false);

        for (int i = 1; i <= armorSegments; i++)
        {
            GameObject newArmourSegment = Instantiate(armorSegment, armorBar.transform);
            newArmourSegment.transform.localPosition = new Vector3(0, 0, 0);
            newArmourSegment.transform.localScale = new Vector3(.04f / armorSegments, .018f, 1f);
        }
    }

    //private void T_UpdateArmourBar()
    //{
        // health bar
       // armourBar.gameObject.SetActive(true);
       // healthBar.fillAmount = currentHealth / stats.MaxHealth;
       // if (hideArmourBarAfterTime != null) StopCoroutine(hideArmourBarAfterTime);
       // hideArmourBarAfterTime = StartCoroutine(HideArmourBarAfterTimeCoroutine(2));
    //}

    private IEnumerator HideArmorBarAfterTimeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        armorBar.gameObject.SetActive(false);
    }

    void SetCurrentArmorBar()
    {
       
        currentArmorBar = armorBar.transform.GetChild(armorSegments -1).GetComponent<RectTransform>();
        currentArmorBarImage = currentArmorBar.GetComponent <Image> ();
        
    }

    public float ReduceCurrentArmor(float incomingDamage)
    {
        

        if (incomingDamage >= 100)
        {
            incomingDamage = 100;
        }

        if (currentArmor % 100 < incomingDamage && currentArmor % 100 != 0)
        {
            incomingDamage = currentArmor % 100;
            currentArmor -= incomingDamage;
            armorSegments--;
            currentArmorBar.localScale = new Vector3(0f, 1f, 1f);
            currentArmorBar.gameObject.GetComponent<Image>().enabled = false;
            SetCurrentArmorBar();
        }



        else
        {
            currentArmor -= incomingDamage;
            currentArmorBar.localScale = new Vector3((armorBarSize * (currentArmor % 100 / 100)), .8f, 1f);
        }

        return currentArmor;
    }
}
