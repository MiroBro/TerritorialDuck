using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Quit()
    {
        //References.Instance.soundHandler.PlayClickBtn();
        Application.Quit();
    }
}
