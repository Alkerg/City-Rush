using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<GameObject> objects;
    public int size;

    void Start()
    {
        InitializePool();
    }

    public void InitializePool()
    {
        for (int i = prefabs.Length - 1; i >= 0; i--)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject obj = Instantiate(prefabs[i], transform.position, Quaternion.identity);
                obj.SetActive(false);
                objects.Add(obj);
            }
        }
    }

    public GameObject GetObject()
    {
        int n;
        do
        {
            n = Random.Range(0, objects.Count);
        } while (objects[n].activeSelf);

        return objects[n];
    }

    public void PlaceObject(Vector3 position)
    {
        for(int i = objects.Count - 1; i >= 0; i--)
        {
            if (!objects[i].activeSelf)
            {
                objects[i].transform.position = position;
                objects[i].SetActive(true);
                return;
            }
        }
        Debug.Log("Object not found");
        GameObject obj = Instantiate(prefabs[0], position, Quaternion.identity);
        objects.Add(obj);
    }
}
