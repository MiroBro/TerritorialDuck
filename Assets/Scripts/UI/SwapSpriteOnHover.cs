using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSpriteOnHover : MonoBehaviour
{
    public GameObject onOnHover;
    public GameObject onOnAway;
    public AudioSource aus;

    public void TurnOnSprite()
    {
        if (onOnAway.activeSelf)
        {
            onOnAway.SetActive(false);
            onOnHover.SetActive(true);
            aus.Play();
        }
    }

    public void TurnOffSprite()
    {
        onOnAway.SetActive(true);
        onOnHover.SetActive(false);
    }
}
