using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool pauseEnabled = false;
    public void TogglePause(InputAction.CallbackContext context)
    {
        if (pauseEnabled)
        {
            Time.timeScale = 1;
            pauseEnabled = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseEnabled = true;
        }

        pauseMenu.SetActive(pauseEnabled);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
