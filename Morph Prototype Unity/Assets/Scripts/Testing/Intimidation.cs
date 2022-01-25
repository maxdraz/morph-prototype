using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intimidation : MonoBehaviour
{
    public float maxIntimidation;
    public float currentIntimidation;
    public float myIntimidationModifier;
    public bool showGizmo;

    public int maxFear;
    public int currentFear;



    // Start is called before the first frame update
    void Start()
    {

        myIntimidationModifier = 1f;

        StartCoroutine("IntimidationCheck");
    }



    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.DrawSphere(this.gameObject.transform.position, currentIntimidation);
        }
    }

    public void SetMaxIntimidation(float totalIntimidation)
    {
        maxIntimidation = totalIntimidation;
        currentIntimidation = maxIntimidation;
        maxFear = Mathf.RoundToInt(maxIntimidation / 100);

    }

    public float SetIntimidationModifier(float intimidationModifier)
    {

        //Debug.Log("adding " + intimidationModifier + " to myIntimidationModifier of " + myIntimidationModifier);
        myIntimidationModifier += intimidationModifier;

        return myIntimidationModifier;
    }

    public int AddFear(int fearToAdd)
    {
        currentFear += fearToAdd;
        return currentFear;
    }

    IEnumerator ModifyIntimidationValue(float value, float duration)
    {
        float amountToChange = (maxIntimidation * 1 + value);

        currentIntimidation += amountToChange;
       
        yield return new WaitForSeconds(duration);

        currentIntimidation -= amountToChange;

        yield return currentIntimidation;

    }

    IEnumerator IntimidationCheck()
    {
        yield return new WaitForSeconds(.5f);
        Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, currentIntimidation);
        foreach (var hitCollider in hitColliders)
        {


            if (hitCollider.gameObject.GetComponent<Intimidation>() == true && hitCollider.gameObject != gameObject)
            {

                float enemyIntimidationValue = hitCollider.gameObject.GetComponent<Intimidation>().currentIntimidation;
                float intimidationToApply = currentIntimidation / (this.gameObject.transform.position - hitCollider.gameObject.transform.position).magnitude;

                if (intimidationToApply > enemyIntimidationValue + (enemyIntimidationValue * .1))
                {
                    //Enemy has been intimidated, they suffer 1 fear
                    hitCollider.gameObject.GetComponent<Intimidation>().AddFear(1);


                    if (intimidationToApply > enemyIntimidationValue * 2)
                    {
                        //Enemy has been severely intimidated, they suffer 2 fear
                        hitCollider.gameObject.GetComponent<Intimidation>().AddFear(2);

                    }
                }

                StartCoroutine("IntimidationCheck");
            }
        }
    }
}
