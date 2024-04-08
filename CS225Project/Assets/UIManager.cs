using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UIManager : MonoBehaviour
{
    public Button buttonToCheck;
    public Canvas canvasToDisable;
    private bool canvasEnabled = true;

    void Start()
    {
        buttonToCheck.onClick.AddListener(ButtonClicked);

        StartCoroutine(CheckButtonStatus());
    }

    void ButtonClicked()
    {
        canvasEnabled = !canvasEnabled;
        Debug.Log("Canvas " + (canvasEnabled ? "enabled" : "disabled") + "!");
    }

    IEnumerator CheckButtonStatus()
    {
        while (true)
        {
            
            if (canvasEnabled)
            {
                
                if (!canvasToDisable.enabled)
                {
                    canvasToDisable.enabled = true;
                    Debug.Log("Canvas enabled!");
                }
            }
            else
            {
               
                if (canvasToDisable.enabled)
                {
                    canvasToDisable.enabled = false;
                    Debug.Log("Canvas disabled!");
                }
            }

            
            yield return null;
        }
    }
}

