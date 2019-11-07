using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
  {
  public int ID;
  // Start is called before the first frame update
  void Start()
    {
    StartCoroutine(EnableAfterSpawn());
    }
  IEnumerator EnableAfterSpawn()
    {
    yield return new WaitForSeconds(.05f);
    GetComponent<BoxCollider>().enabled = true;
    }
  private void OnTriggerEnter(Collider other)
    {
    if(other.gameObject.tag == "Player")
      {
      var playerScript = other.GetComponent<PlayerManager>();
      playerScript.Die();
      }
    }
  }
