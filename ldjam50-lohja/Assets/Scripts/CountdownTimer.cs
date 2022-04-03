using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;

    // Update is called once per frame
    void Update()
    {
        // DisplayTime(timeToDisplay)
    }

    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int milliseconds = Mathf.FloorToInt(timeToDisplay % 1f * 1000);
    }
}
