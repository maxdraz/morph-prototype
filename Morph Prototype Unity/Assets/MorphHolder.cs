using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public bool addMorphs;
    public bool getEnums;

    public Morph[] morphsInChildren;
    public MorphLoadout loadout;

    private void Start()
    {
        morphsInChildren = GetComponentsInChildren<Morph>();

        if (addMorphs)
            Invoke ("AddChildMorphs", 3);

        if (getEnums)
            Invoke("GetChildMorphEnums", 1);
    }
    void AddChildMorphs()
    {
        foreach (Morph morph in morphsInChildren)
        {
            Debug.Log("Trying to add " + morph.name + " to morphLoadout");
            loadout.AddMorphToLoadoutAtRuntime(morph);
        }
    }

    //void GetChildMorphEnums() 
    //{
    //foreach (Morph morph in morphsInChildren)
    //    {
    //        Debug.Log("Getting morphtype for " + morph.name);
    //        morph.GetComponent<Morph>().GetMorphType();
    //    }
    //}
}
