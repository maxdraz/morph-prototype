using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphCollectionScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           ToggleUI();
        }
    }

    void ToggleUI()
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

           PauseGame(false);
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
                
            PauseGame(true);
        }
    }

    void PauseGame(bool shouldPause)
    {
        if (shouldPause)
        {
            PlayerCreatureCharacter.Instance.CanAcceptInput = false;
            CursorManager.SetCursorLockMode(CursorLockMode.None);
            return;
        }
        
        PlayerCreatureCharacter.Instance.CanAcceptInput = true;
        CursorManager.SetCursorLockMode(CursorLockMode.Locked);
    }
}
