using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortitude : MonoBehaviour
{
    float maxFortitude;
    float currentFortitude;
    float lastFortitudeValue;

    //Decimal value: < 1, > 0
    public float fortitudeDamagePercentResistance;

    public float fortitudeDamageFlatResistance;
    
    //Decimal value: < 1, > 0
    public float statusEffectDurationReduction;

    float fortitudeRegenDelay = 2f;
    float fortitudeRegenRate = 1f;

    float secondaryStat;

    float chanceTobeEffected;
    bool statusApplied;

    Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        statusApplied = false;
        currentFortitude = maxFortitude;
    }

    

    public float ApplyFortitudeDamage(FortitudeDamageData data)
    {
        float fortDamage = data.FortitudeDamage;
        string effect = data.StatusEffect;
        float duration = data.Duration;
        
        currentFortitude -= data.FortitudeDamage;

        if (currentFortitude <= 0)
        {


            if (data.StatusEffect == "Stun")
            {
                secondaryStat = 0f;
                //instead the dc is (fortDamage - (lastFortitudeValue * 3))
            }

            if (data.StatusEffect == "Paralysis")
            {
                secondaryStat = stats.totalAgility;
            }

            if (data.StatusEffect == "Root")
            {
                secondaryStat = 0f;
                //instead the dc is (fortDamage - (lastFortitudeValue * 3))
            }

            if (data.StatusEffect == "Silence")
            {
                secondaryStat = stats.totalIntelligence;
            }

            if (data.StatusEffect == "Crippled")
            {
                secondaryStat = stats.totalToughness;
            }

            if (secondaryStat == 0)
            {
                //might need to be changed to lastFortitudeValue + toughness
                chanceTobeEffected = Mathf.Max(fortDamage - (lastFortitudeValue * 3), 0);
            }
            else
            {
                chanceTobeEffected = Mathf.Max(fortDamage - (lastFortitudeValue + secondaryStat), 0);
            }

            if (chanceTobeEffected > 0)
            {
                StatusCheck(chanceTobeEffected, effect, duration);
            }
        }

        currentFortitude = 0f;
        StartCoroutine("FortitudeRegen");
        return currentFortitude;
    }

    IEnumerator FortitudeRegen() 
    {
        
        fortitudeRegenRate = 0f;

        yield return new WaitForSeconds(fortitudeRegenDelay);

        fortitudeRegenRate = 1f;
    }

    public void ImmediateCC(float DC, string effect, float duration)
    {
        if (effect == "stun")
        {
            secondaryStat = 0f;
            //instead the chanceToSuccumb is (DC - (maxFortitude * 1.5))
        }

        if (effect == "paralysis")
        {
            //float secondaryStat = agility
        }

        if (effect == "root")
        {
            secondaryStat = 0f;
            //instead the chanceToSuccumb is (DC - (maxFortitude * 1.5))
        }

        if (effect == "silence")
        {
            secondaryStat = stats.totalIntelligence;
        }

        if (effect == "crippled")
        {
            secondaryStat = stats.totalToughness;
        }

        if (secondaryStat == 0)
        {
            chanceTobeEffected = (DC - (maxFortitude * 1.5f));
        }
        else
        {
            chanceTobeEffected = (DC - (maxFortitude + secondaryStat));
        }

        if (chanceTobeEffected > 0)
        {
            StatusCheck(chanceTobeEffected, effect, duration);
        }
    }

    void StatusCheck(float chance, string statusToApply, float duration)
    {
        float randomNumber = (Random.value) * 100;

        if (randomNumber <= chanceTobeEffected)
        {
            statusApplied = true;
        }

        if (statusApplied)
        {
            StatusEffect(statusToApply, duration);
            Debug.Log(statusToApply + " had " + chance + " chance to happen, and succeeded with " + randomNumber);
        }

        else
        {
            Debug.Log(statusToApply + " had " + chance + " chance to happen, and failed with " + randomNumber);
        }

        statusApplied = false;
    }

    void StatusEffect(string statusToApply, float duration)
    {

        if (statusToApply == "stun")
        {
            StartCoroutine("Stun", duration); 
        }

        if (statusToApply == "paralysis")
        {
            Paralysis(duration);
        }

        if (statusToApply == "root")
        {
            Root(duration);
        }

        if (statusToApply == "silence")
        {
            Silence(duration);
        }

        if (statusToApply == "crippled")
        {
            Crippled(duration);
        }

    }

    IEnumerator Stun(float duration) 
    {
        //Turn off movement and attack capabilities (weapon and special morphs)

        yield return new WaitForSeconds(duration);

        //Turn movement and attack capabilities back on

        yield return null;
    }

    IEnumerator Blindness(float duration)
    {
        //Reduce perception and therefore accuracy stat to 0

        yield return new WaitForSeconds(duration);

        //Return perception to whatever value it should be at this moment

        yield return null;
    }

    IEnumerator Paralysis(float duration)
    {
        //Reduce total move speed by 75%, reduce total attack speed by 50%, and disable all mobility

        yield return new WaitForSeconds(duration);

        //Return Agility and total attacks speeds to their respective values (Value + previously removed value), and enable mobility

        yield return null;
    }

    IEnumerator Root(float duration)
    {
        //Turn off movement and remove mobility options

        yield return new WaitForSeconds(duration);

        //Turn movement back on and enable mobility options

        yield return null;
    }

    IEnumerator Crippled(float duration)
    {
        //Similar to intimidation effect 'shaken' (- 20% damage dealt, -20% intimidation on defense, -20% energy gain from all sources)
        //also slows target by 20-30% for the duration

        yield return new WaitForSeconds(duration);

        //remove the feects shown above

        yield return null;
    }

    IEnumerator Silence(float duration)
    {
        //Disable all active special morphs (any active abilities on special morphs) 

        yield return new WaitForSeconds(duration);

        //Enable all active special morphs (any active abilities on special morphs) 

        yield return null;
    }



    // Update is called once per frame
    void Update()
    {
        maxFortitude = stats.totalFortitude;

        if (currentFortitude > maxFortitude) 
        {
            currentFortitude = maxFortitude;
        }

        if (currentFortitude < maxFortitude && fortitudeRegenRate > 0) 
        {
            currentFortitude = currentFortitude + (10 * fortitudeRegenRate) * Time.deltaTime;
        }

        //if (Input.GetKeyDown("left shift")) 
        //{
            //chanceTobeEffected = (Random.value) * 100;
            //chanceTobeEffected = 30;
            //StatusCheck(chanceTobeEffected, "stun");
        //}
    }
}
