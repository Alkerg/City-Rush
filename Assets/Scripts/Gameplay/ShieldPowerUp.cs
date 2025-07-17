using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public PowerUpManager powerUpManager;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        powerUpManager = FindAnyObjectByType<PowerUpManager>().GetComponent<PowerUpManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
    }
    private void Update()

    {
        if (!LevelManager.isGameRunning) return;
        transform.position += new Vector3(0, 0, -LevelManager.levelSpeed * Time.deltaTime);

        if (transform.position.z <= -7f) gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            powerUpManager.ActiveShieldPowerUp();
            meshRenderer.enabled = false;
        }
    }
}
