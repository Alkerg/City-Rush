using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGenerationManager : MonoBehaviour
{
    public Pool obstaclePool;
    public float timeBetweenObstacles = 1f;
    private Dictionary<int, int[]> possiblePositions = new Dictionary<int, int[]>
    {
        {1, new int[] {1, 0, 0} },
        {2, new int[] {0, 1, 0} },
        {3, new int[] {0, 0, 1} },
        {4, new int[] {0, 1, 1} },
        {5, new int[] {1, 1, 0} },
        {6, new int[] {1, 1, 1} },
        {7, new int[] {1, 0, 1} },
    };

    public void PlaceInRail(int position, GameObject obstacle)
    {
        float xPos = 0;
        
        switch (position)
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
        obstacle.SetActive(true);
        obstacle.transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    public void StartGeneration()
    {
        InvokeRepeating(nameof(GenerateObstacles),0, timeBetweenObstacles);
    }

    public void GenerateObstacles()
    {
        int[] railsPosition = possiblePositions[Random.Range(1, 8)];

        for(int i=0; i < 3; i++)
        {
            if (railsPosition[i] != 0)
            {
                GameObject obstacle = obstaclePool.GetObject();
                PlaceInRail(i,obstacle);
            }
            
        }
    }

}
