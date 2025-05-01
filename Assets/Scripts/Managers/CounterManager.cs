using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterManager : MonoBehaviour
{
    public CharacterSelectionManager characterSelectionManager;
    public Animator playerAnimator;
    public TextMeshProUGUI distanceTMP;
    public float distance;
    public ObjectsGenerationManager obstacleManager;
    public ObjectsGenerationManager coinManager;
    public float distanceBreach = 10f;
    public AudioSource audioSource;
    void Start()
    {
        distance = 0;
    }

    void Update()
    {
        if (!LevelManager.isGameRunning) return;

        if(distance >= distanceBreach)
        {
            distanceBreach += 20f;
            LevelManager.levelSpeed *= 1.1f;
            obstacleManager.timeBetweenObstacles *= 0.75f;
        }
        distance += Time.deltaTime;
        distanceTMP.text = distance.ToString("0.000") + "m";
    }

    public void StartCounterAndPlayer()
    {
        SetPlayerAnimator();
        LevelManager.isGameRunning = true;
        playerAnimator.SetBool("Running",true);
        obstacleManager.StartGeneration();
        coinManager.StartGeneration();
        audioSource.Play();
    }

    public void SetPlayerAnimator()
    {
        playerAnimator = characterSelectionManager.GetCurrentCharacterAnimator();
    }
}
