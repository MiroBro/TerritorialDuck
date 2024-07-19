using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockHandler : MonoBehaviour
{
    public float currentTime;
    public int startMinutes;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime + Time.deltaTime;

        System.TimeSpan time = System.TimeSpan.FromSeconds(currentTime);
        timerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }
}
