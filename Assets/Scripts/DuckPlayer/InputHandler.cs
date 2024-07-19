using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (References.Instance.uiToggler.showNewItemUI.activeSelf && KeyDown())
        {
            //References.Instance.uiToggler.EnableStandardUI();
            References.Instance.uiToggler.CloseNewItemDisplay();
        } else if (References.Instance.uiToggler.introUI.activeSelf && KeyDown())
        {
            References.Instance.uiToggler.CloseIntroUI();
        } 
        else if (Input.GetKeyDown(KeyCode.Escape) && References.Instance.uiToggler.settingsUI.activeSelf)//!BuildHandler.ableToBuild)
        {
            References.Instance.uiToggler.ToggleSettingsUI();
        }
    }

    private bool KeyDown()
    {
        return (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2));
    }
}
