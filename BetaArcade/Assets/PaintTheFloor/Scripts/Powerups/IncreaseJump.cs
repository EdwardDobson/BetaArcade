using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseJump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMove>().AddBigJumps(3);
            Destroy(gameObject);
        }
    }
}
