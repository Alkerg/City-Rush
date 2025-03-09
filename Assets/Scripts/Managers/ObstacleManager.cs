using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public Pool obstaclePool;
    public float timeBetweenObstacles = 1f;

    public void PlaceInRail(int[] rails, GameObject obstacle)
    {
        int rail = Random.Range(0, 3);
        float xPos = 0;
        if (rails[rail] == 0)
        {
            switch (rail)
            {
                case 0:
                    xPos = -Player.railDistance; //-1.43
                    break;
                case 1:
                    xPos = 0;
                    break;
                case 2:
                    xPos = Player.railDistance; //1.43
                    break;
            }
            rails[rail] = 1;
            obstacle.SetActive(true);
            obstacle.transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
     
    }

    public void StartObstacles()
    {
        InvokeRepeating(nameof(GenerateObstacles),0, timeBetweenObstacles);
    }

    public void GenerateObstacles()
    {
        int numberOfObstacles = Random.Range(1, 4);
        int[] rails = { 0, 0, 0 };

        for(int i=0; i < numberOfObstacles; i++)
        {
            GameObject obstacle = obstaclePool.GetObject();
            PlaceInRail(rails, obstacle);
        }
    }

}
