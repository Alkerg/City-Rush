using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Pool pool;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            pool.PlaceObject(new Vector3(37.6762619f, 25.1231441f, 74.138f));
        }
    }
}
