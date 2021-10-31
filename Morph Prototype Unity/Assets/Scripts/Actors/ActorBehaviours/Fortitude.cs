using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortitude : MonoBehaviour
{
    float maxFortitude;
    float currentFortitude;

    float fortitudeRegenDelay = 2f;
    float fortitudeRegenRate = 1f;

    float secondaryStat;

    float chanceTobeEffected;
    bool statusApplied;

    // Start is called before the first frame update
    void Start()
    {
        statusApplied = false;
        currentFortitude = maxFortitude;
    }

    public float ReduceFortitude(int fortDamage, string effect) 
    {
        
        float lastFortitudeValue = currentFortitude;
        currentFortitude -= fortDamage;
        DisableFortitudeRegen();

        if (currentFortitude <= 0) 
        {
            

            if (effect == "Stun") 
            {
                float secondaryStat = 0f;
                //instead the dc is (fortDamage - (lastFortitudeValue * 3))
            }

            if (effect == "Paralysis")
            {
                //float secondaryStat = agility
            }

            if (effect == "Root")
            {
                float secondaryStat = 0f; 
                //instead the dc is (fortDamage - (lastFortitudeValue * 3))
            }

            if (effect == "Silence")
            {
                //float secondaryStat = intelligence
            }

            if (effect == "Crippled")
            {
                //float secondaryStat = toughness
            }

            if (secondaryStat == 0)
            {
                chanceTobeEffected = (fortDamage - (lastFortitudeValue * 3));
            }
            else 
            {
                chanceTobeEffected = (fortDamage - (lastFortitudeValue + secondaryStat));
            }


            StatusCheck(chanceTobeEffected, effect);
  
        }

        Invoke("FortitudeRegen", fortitudeRegenDelay);
        return currentFortitude;
    }

    void StatusCheck(float chance, string statusToApply) 
    {
        float randomNumber = Random.value;

        if (randomNumber <= chanceTobeEffected)
        {
            statusApplied = true;
        }

        if (statusApplied)
        {
            StatusEffect(statusToApply);
        }
    }

    void FortitudeRegen() 
    {
        fortitudeRegenRate = 1f;
    }

    void DisableFortitudeRegen()
    {
        fortitudeRegenRate = 0f;
    }

    void StatusEffect(string statusToApply) 
    {

        if (statusToApply == "Stun") 
        {
        
        }

        if (statusToApply == "Paralysis")
        {

        }

        if (statusToApply == "Root")
        {

        }

        if (statusToApply == "Silence")
        {

        }

        if (statusToApply == "Crippled")
        {
            
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (currentFortitude > maxFortitude) 
        {
            currentFortitude = maxFortitude;
        }

        if (currentFortitude < maxFortitude && fortitudeRegenRate > 0) 
        {
            currentFortitude = currentFortitude + (10 * fortitudeRegenRate) * Time.deltaTime;
        }
    }
}
