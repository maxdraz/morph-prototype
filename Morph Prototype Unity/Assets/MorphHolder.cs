using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public bool addMorphs;

    Morph[] morphsInChildren;
    public MorphLoadout player;

    private void Start()
    {
        morphsInChildren = GetComponentsInChildren<Morph>();

        if (addMorphs)
        Invoke ("AddChildMorphs", 1);
    }
    void AddChildMorphs()
    {
        foreach (Morph morph in morphsInChildren)
        {
            player.AddMorphToLoadoutAtRuntime(morph);
        }
    }
}
