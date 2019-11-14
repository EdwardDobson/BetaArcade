using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
  {
  void Start()
    {
    StartCoroutine(DestroyAfterTime(1));
    }

  IEnumerator DestroyAfterTime(float delay)
    {
    yield return new WaitForSeconds(delay);
    Destroy(gameObject);
    }
  }
