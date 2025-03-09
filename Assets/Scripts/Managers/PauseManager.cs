using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuPanel;

    void Awake()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;
        pauseMenuPanel.SetActive(isGamePaused);
        if (isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void ExitLevel()
    {
        ScenesManager.LoadScene(0);
    }
}
