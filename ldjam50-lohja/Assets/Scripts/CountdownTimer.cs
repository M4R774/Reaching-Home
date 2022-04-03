using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float remainingTime = 42f;
    public Text countdownText;
    public World world;

    private int oldHealthyEngineCount = 6;

    // Update is called once per frame
    void Update()
    {
        checkIfNumberOfWorkingEnginesHasChangedAndChangeETAAccordingly();

        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            DisplayTime(0f);
            SceneManager.LoadScene(2);
        }
        else
        {
            DisplayTime(remainingTime);
        }
    }

    private void checkIfNumberOfWorkingEnginesHasChangedAndChangeETAAccordingly()
    {
        int healthyEngineCount = 0;
        foreach (EngineTask engineTask in world.engines)
        {
            if (engineTask.healthy)
            {
                healthyEngineCount++;
            }
        }
        if (healthyEngineCount != oldHealthyEngineCount)
        {
            RecalculateRemainingTime(healthyEngineCount);
        }
        oldHealthyEngineCount = healthyEngineCount;
    }

    private void RecalculateRemainingTime(int healthyEngineCount)
    {
        float oldSpeed = oldHealthyEngineCount / 6f;
        float newSpeed = healthyEngineCount / 6f;
        float speedChangePercentage = newSpeed / oldSpeed;
        remainingTime = remainingTime / speedChangePercentage;
    }

    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int milliseconds = Mathf.FloorToInt(timeToDisplay % 1f * 100);
        countdownText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}
