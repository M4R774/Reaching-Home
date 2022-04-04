using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeSupportMetaTask : MonoBehaviour
{
    public GameObject LifeSupportTimer;
    public List<GameObject> levers;
    public float oxygenTimeRemaining;

    public void Start()
    {
        LifeSupportTimer.SetActive(false);
    }

    public void BreakALLLifeSupportModules()
    {
        foreach (GameObject lever in levers)
        {
            lever.GetComponent<Task>().ChaosInteract();
        }
    }

    public void FixedUpdate()
    {
        bool allSystemsHealthy = true;
        foreach (GameObject lever in levers)
        {
            if(!lever.GetComponent<Task>().healthy)
            {
                allSystemsHealthy = false;
            }
        }

        if (!allSystemsHealthy)
        {
            oxygenTimeRemaining -= Time.deltaTime;
            DisplayTime(oxygenTimeRemaining);
            LifeSupportTimer.SetActive(true);
            if (oxygenTimeRemaining < 0)
            {
                SceneManager.LoadScene(3);
            }
        }
        else
        {
            oxygenTimeRemaining = 60;
            LifeSupportTimer.SetActive(false);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int milliseconds = Mathf.FloorToInt(timeToDisplay % 1f * 100);
        LifeSupportTimer.GetComponent<Text>().text = $"Remaining\noxygen:\n{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}
