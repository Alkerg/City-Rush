using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public int coins = 0;
    public TextMeshProUGUI coinsTMP;

    public void AddCoin()
    {
        coins++;
        coinsTMP.text = coins.ToString();
    }
}
