using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float remainingTime = 642f;
    public Text countdownText;

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        DisplayTime(remainingTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int milliseconds = Mathf.FloorToInt(timeToDisplay % 1f * 100);
        countdownText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}
