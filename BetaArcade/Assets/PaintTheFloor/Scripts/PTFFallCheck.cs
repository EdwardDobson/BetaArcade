using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTFFallCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "FallCheck")
        {
            gameObject.transform.position = new Vector3(0, 1.5f, -4);
        }
    }
}
