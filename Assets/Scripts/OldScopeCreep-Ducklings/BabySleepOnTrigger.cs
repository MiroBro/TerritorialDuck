using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySleepOnTrigger : MonoBehaviour
{
    public GameObject babyCollided;
    public GameObject babySleeping;
    public float sleepMinInterval;

    public bool canSleep = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canSleep)
        {
            if (collision.CompareTag("Baby"))
            {
                canSleep = false;
                MoveBaby.ResetPath();
                babyCollided = collision.gameObject;
                babyCollided.SetActive(false);

                babySleeping.SetActive(true);
                StartCoroutine(Wait(3, EnableBabyAwake));
            }
        }            
    }

    private void EnableBabyAwake()
    {
        babyCollided.SetActive(true);
        babySleeping.SetActive(false);
    }

    IEnumerator Wait(float seconds, Action doAfter)
    {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(seconds);
        doAfter();
        yield return new WaitForSecondsRealtime(sleepMinInterval);
        canSleep = true;
    }

}
