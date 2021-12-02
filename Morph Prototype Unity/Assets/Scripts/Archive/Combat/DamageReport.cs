using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReport : MonoBehaviour
{
    Color Ice = new Color(0, 1, 1, 1);
    Color Fire = new Color(1, 0.3f, 0f, 1);
    Color Electric = new Color(1, .92f, .16f, 1);
    Color Poison = new Color(1, 0, 1, 1);
    Color Acid = new Color(0, 1, 0, 1);

    float randomForce;
    public float damage;

    //DamageType is a string right now but must be reworked to use 'damageType' enum
    public string damageType;

    // Start is called before the first frame update
    void Start()
    {
        //Add some random direction and upward speed to the object
        randomForce = Random.Range(2,-2);
        Vector3 spawnDirection = new Vector3(randomForce , 5, 0);
        GetComponent<Rigidbody>().AddForce(spawnDirection,ForceMode.Impulse);

        GetComponent<TMPro.TextMeshPro>().text = damage.ToString();

        //Add to the size of the text based on the amount of damage
        if (damage > 75) 
        {
            float differential = (damage - 75) / 10;
            int extraFontSize = (int)differential;
            GetComponent<TMPro.TextMeshPro>().fontSize += extraFontSize;
        }

        //Set the text colour to match the damage type
        if (damageType == "Ice") 
        {
            GetComponent<TMPro.TextMeshPro>().color = Ice;
        }

        if (damageType == "Fire")
        {
            GetComponent<TMPro.TextMeshPro>().color = Fire;
        }

        if (damageType == "Electric")
        {
            GetComponent<TMPro.TextMeshPro>().color = Electric;
        }

        if (damageType == "Poison")
        {
            GetComponent<TMPro.TextMeshPro>().color = Poison;
        }

        if (damageType == "Acid")
        {
            GetComponent<TMPro.TextMeshPro>().color = Acid;
        }

        if (damageType == "Bludgeoning")
        {
            GetComponent<TMPro.TextMeshPro>().color = Color.grey;
        }

        if (damageType == "Piercing")
        {
            GetComponent<TMPro.TextMeshPro>().color = Color.black;
            GetComponent<TMPro.TextMeshPro>().outlineColor = Color.white;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
