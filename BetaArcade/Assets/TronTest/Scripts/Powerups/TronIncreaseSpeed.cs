using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TronIncreaseSpeed : MonoBehaviour
  {
  private void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.tag.Contains("Player"))
      {
      other.GetComponent<PlayerManager>().Speedup();
      Destroy(gameObject);
      }
    }
  }
