using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserDataDisplay : MonoBehaviour
{
    public TextMeshProUGUI usernameTMP;
    public TextMeshProUGUI coinsTMP;
    public TextMeshProUGUI bestScoreTMP;

    async void Start()
    {
        string username = await DBManager.Instance.GetPlayerUsername();
        int coins = await DBManager.Instance.GetCoins();
        float bestScore = await DBManager.Instance.GetBestScore();

        usernameTMP.text = username;
        coinsTMP.text = coins.ToString();
        bestScoreTMP.text = bestScore.ToString();
    }
}
