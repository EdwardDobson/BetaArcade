using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFireRate : MonoBehaviour
  {
  private void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.tag == "Player")
      {
      other.gameObject.GetComponent<PTFMovement>().FireRate *= 2;
      Destroy(gameObject);
      }
    }
  }
