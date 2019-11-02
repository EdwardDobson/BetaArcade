using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseShotSize : MonoBehaviour
  {
  private void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.tag == "Player")
      {
      other.gameObject.GetComponent<PTFMovement>().ShotSize = 5;
      Destroy(gameObject);
      }
    }
  }
