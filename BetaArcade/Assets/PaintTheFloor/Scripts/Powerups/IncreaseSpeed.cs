using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeed : MonoBehaviour
  {
  private void OnTriggerEnter(Collider other)
    {
    if(other.gameObject.tag == "Player")
      {
      other.gameObject.GetComponent<PlayerMove>().IncreaseMovementSpeed();
      Destroy(gameObject);
      }
    }
  }
