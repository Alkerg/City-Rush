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
    public CoinsManager coinsManager;

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
            VerifyAndUpdateScore();
            UpdateCoins();
        }
    }

    public void StartCameraAnimation()
    {
        cameraAnimator.SetTrigger("StartGame");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        counterManager.audioSource.Stop();
    }

    private async void VerifyAndUpdateScore()
    {
        float currentScore = await DBManager.Instance.GetBestScore();
        float newScore = counterManager.distance;

        Debug.Log("Current score:" + currentScore);
        Debug.Log("New score:" + newScore);

        if (newScore > currentScore)
        {
            DBManager.Instance.UpdateBestScore(newScore);
            Debug.Log($"Score updated: {newScore}");
        }
    }

    private async void UpdateCoins()
    {
        int coinsObtained = coinsManager.coins;
        int currentCoins = await DBManager.Instance.GetCoins();
        int newCoins = coinsObtained + currentCoins;
        DBManager.Instance.UpdateCoins(newCoins);
        Debug.Log($"Coins updated: {newCoins}");
    }

    public async void SignOutPlayer()
    {
        DBManager.Instance.DeleteSession();
        await DBManager.Instance.SignOut();
        ScenesManager.LoadScene(0);
    }
}
