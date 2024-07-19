using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEvilHappyImage : MonoBehaviour
{
    public GameObject happyOpen;
    public GameObject happyClosed;

    public GameObject happyDesc;
    public GameObject evilDesc;

    public GameObject glow;

    public void TurnOnHappy()
    {
        evilDesc.SetActive(false);
        happyDesc.SetActive(true);

        happyOpen.SetActive(true);
        happyClosed.SetActive(false);
        glow.SetActive(false);
    }

    public void TurnOnEvil()
    {
        evilDesc.SetActive(true);
        happyDesc.SetActive(false);

        happyClosed.SetActive(true);
        happyOpen.SetActive(false);
        glow.SetActive(true);
    }

    public void TurnOffHappyText()
    {
        happyDesc.SetActive(false);
    }

    public void TurnOffChaseText() {
        evilDesc.SetActive(false);
    }
}
