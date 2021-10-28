using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortitude : MonoBehaviour
{
    int maxFortitude;
    int currentFortitude;

    int fortSaveDC;

    // Start is called before the first frame update
    void Start()
    {
        currentFortitude = maxFortitude;
    }

    public int ReduceFortitude(int fortDamage, string effect) 
    {
        currentFortitude -= fortDamage;

        if (currentFortitude <= 0) 
        {
            //reduced to 0 fortitude
            fortSaveDC = fortDamage;
            float ChanceToBeEffected = (fortSaveDC / maxFortitude) * 100;

            StatusEffect(ChanceToBeEffected, effect);
        }

        return currentFortitude;
    }

    void StatusEffect(float chance, string statusToApply) 
    {
    
    }

    // Update is called once per frame
    void Update()
    {

        if (currentFortitude > maxFortitude) 
        {
            currentFortitude = maxFortitude;
        }
        
    }
}
