using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    public GameObject magnetTrigger;
    public GameObject shieldTrigger;
    public TextMeshProUGUI powerUpTMP;
    
    public void ActiveMagnetPowerUp()
    {
        StartCoroutine(ActiveGetCoinTriggerForSeconds());
    }

    public void ActiveShieldPowerUp()
    {
        StartCoroutine(ActiveShieldPowerUpForSeconds());
    }

    IEnumerator ActiveGetCoinTriggerForSeconds()
    {
        powerUpTMP.text = "Magnet Activated";
        magnetTrigger.SetActive(true);
        yield return new WaitForSeconds(5);
        powerUpTMP.text = "";
        magnetTrigger.SetActive(false);
    }

    IEnumerator ActiveShieldPowerUpForSeconds()
    {
        powerUpTMP.text = "Shield Activated";
        shieldTrigger.SetActive(true);
        yield return new WaitForSeconds(5);
        powerUpTMP.text = "";
        shieldTrigger.SetActive(false);
    }
}
