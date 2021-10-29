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

    // Start is called before the first frame update
    void Start()
    {

        currentFortitude = maxFortitude;
    }

    public float ReduceFortitude(int fortDamage, string effect) 
    {
        float lastFortitudeValue = currentFortitude;
        currentFortitude -= fortDamage;
        DisableFortitudeRegen();

        if (currentFortitude <= 0) 
        {
            bool statusApplied = false;

            if (effect == "Stun") 
            {
                //float secondaryStat = null instead the dc is (fort * 1.5)
            }

            if (effect == "Paralysis")
            {
                //float secondaryStat = agility
            }

            if (effect == "Root")
            {
                //float secondaryStat = null instead the dc is (fort * 1.5)
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
                float ChanceToBeEffected = (fortDamage - (lastFortitudeValue * 3));
            }
            else 
            {
                float ChanceToBeEffected = (fortDamage - (lastFortitudeValue + secondaryStat));
            }

            
            //need to roll to see if the status has been applied, if so statusApplied bool needs to be true, else false


            if (statusApplied) 
            {
                StatusEffect(effect);
            }
        }

        Invoke("FortitudeRegen", fortitudeRegenDelay);
        return currentFortitude;
    }

    public void StatusCheck(float chance, string statusToApply) 
    {
    
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
