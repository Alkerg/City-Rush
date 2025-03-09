using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public Animator cameraAnimator;
    public static bool isGameRunning = false;
    public static bool isGameOver = false;
    public static float levelSpeed = 3;
    public GameObject gameOverPanel;
    public DBManager DBManager;
    public CounterManager counterManager;

    void Awake()
    {
        isGameRunning = false;
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            GameOver();
            isGameOver = false;
        }
    }

    public void StartCameraAnimation()
    {
        cameraAnimator.SetTrigger("StartGame");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        float currentScore = PlayerPrefs.GetFloat("score");
        float newScore = counterManager.distance;
        gameOverPanel.SetActive(true);
        VerifyAndUpdateScore(currentScore, newScore);
        counterManager.audioSource.Stop();
    }

    private void VerifyAndUpdateScore(float currentScore, float newScore)
    {
       /* Debug.Log("currentScore:" + currentScore);
        Debug.Log("newScore:" + newScore);
        if (newScore > currentScore)
        {
            DBManager.UpdateScore(newScore);
            PlayerPrefs.SetFloat("score", newScore);
            Debug.Log("Score updated");
        }*/
    }

}
