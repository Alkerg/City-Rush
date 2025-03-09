using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    void Update()
    {
        if (!LevelManager.isGameRunning) return;
        transform.position += new Vector3(0, 0, -LevelManager.levelSpeed * Time.deltaTime);

        if(transform.position.z <= -48) gameObject.SetActive(false);
    }
}
