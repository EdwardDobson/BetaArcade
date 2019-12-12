using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAMFallReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "FallCheck")
        {
            System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
            transform.position = new Vector3(rand.Next(-3, 3), 1, rand.Next(-3, 3));
        }
    }
}
