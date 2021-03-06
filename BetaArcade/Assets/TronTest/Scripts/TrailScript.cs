﻿using System.Collections;
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
    // Check if trigger collision is with a player
    if(other.gameObject.tag.Contains("Player"))
      {
      var playerScript = other.GetComponent<PlayerManager>();
      if (!playerScript.IsDead)
        {
        playerScript.Die();
        }
      }
    }
  }
