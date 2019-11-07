using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains("Player"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
