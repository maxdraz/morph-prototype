using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Armor : MonoBehaviour
{
    [SerializeField] private float baseMaxArmour;
    public float bonusFlatMaxArmour;
    public float bonusPercentMaxArmour;
    [SerializeField] private float maxArmour;


    public float currentArmour;
    public bool HasArmor => currentArmour > 0;

    public int armourSegments;
    public GameObject armourSegment;
    RectTransform currentArmourBar;
    float armourBarSize;

    [SerializeField] private Transform armourBar;
    private Coroutine hideArmourBarAfterTime;
    Image currentArmourBarImage;

    Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();

        T_SetUpArmourbar();
        SetMaxArmour();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetMaxArmour()
    {
        baseMaxArmour = stats ? stats.MaxArmour : 100; 
        maxArmour = (baseMaxArmour * (1+bonusPercentMaxArmour))+ bonusFlatMaxArmour;

        float armourRemainder = maxArmour % 100;
        maxArmour -= armourRemainder;
        currentArmour = maxArmour;

        int armourSegments = (int)maxArmour / 100;

        //float armourBarSize = (healthBarSize / armourSegments) * 2;

        for (int i = 1; i <= armourSegments; i++)
        {
            GameObject newArmourSegment = Instantiate(armourSegment, armourBar.transform);
            newArmourSegment.transform.localPosition = new Vector3(0, 0, 0);
            newArmourSegment.transform.localScale = new Vector3(.04f/armourSegments, .018f, 1f);
        }

        T_SetUpArmourbar();
    }

    private void T_SetUpArmourbar()
    {
        armourBar = transform.Find("HealthBarCanvas").Find("ArmourBar");
        armourBar.gameObject.SetActive(false);
    }

    //private void T_UpdateArmourBar()
    //{
        // health bar
       // armourBar.gameObject.SetActive(true);
       // healthBar.fillAmount = currentHealth / stats.MaxHealth;
       // if (hideArmourBarAfterTime != null) StopCoroutine(hideArmourBarAfterTime);
       // hideArmourBarAfterTime = StartCoroutine(HideArmourBarAfterTimeCoroutine(2));
    //}

    private IEnumerator HideArmourBarAfterTimeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        armourBar.gameObject.SetActive(false);
    }

    void SetCurrentArmourBar()
    {
       
        currentArmourBar = armourBar.transform.GetChild(armourSegments -1).GetComponent<RectTransform>();
        currentArmourBarImage = currentArmourBar.GetComponent <Image> ();
        
    }

    public float ReduceCurrentArmour(float incomingDamage)
    {
        

        if (incomingDamage >= 100)
        {
            incomingDamage = 100;
        }

        if (currentArmour % 100 < incomingDamage && currentArmour % 100 != 0)
        {
            incomingDamage = currentArmour % 100;
            currentArmour -= incomingDamage;
            armourSegments--;
            currentArmourBar.localScale = new Vector3(0f, 1f, 1f);
            currentArmourBar.gameObject.GetComponent<Image>().enabled = false;
            SetCurrentArmourBar();
        }



        else
        {
            currentArmour -= incomingDamage;
            currentArmourBar.localScale = new Vector3((armourBarSize * (currentArmour % 100 / 100)), .8f, 1f);
        }

        return currentArmour;
    }
}
