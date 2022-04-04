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
    public GameObject computerExplanationText;

    private int oldHealthyEngineCount = 6;

    // Update is called once per frame
    void Update()
    {
        checkIfNumberOfWorkingEnginesHasChangedAndChangeETAAccordingly();

        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            DisplayTime(0f);
            countdownText.color = Color.red;
            if(AllComputersAreHealthy())
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                computerExplanationText.SetActive(true);
            }
            
        }
        else
        {
            DisplayTime(remainingTime);
        }
    }

    private bool AllComputersAreHealthy()
    {
        foreach(ComputerTask computer in world.computers)
        {
            if(computer.healthy)
            {
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
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
        countdownText.text = $"ETA:{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}
