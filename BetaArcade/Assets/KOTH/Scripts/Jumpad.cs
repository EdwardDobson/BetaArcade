using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpad : MonoBehaviour
{
    public float jumpBoost;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpBoost);
        }
    }
}
