using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballScript : MonoBehaviour
  {
  public Color Color;
  private void OnTriggerEnter(Collider other)
    {
    var color = gameObject.GetComponent<Renderer>().material.GetColor("_Color");

    var renderer = other.gameObject.GetComponent<Renderer>();
    if (renderer != null)
      {
      GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

      foreach (var player in players)
        {
        if (player.GetComponent<Renderer>().material.GetColor("_Color") == renderer.material.GetColor("_Color"))
          player.GetComponent<PTFMovement>().Score--;
        if (player.GetComponent<Renderer>().material.GetColor("_Color") == color)
          player.GetComponent<PTFMovement>().Score++;
        }

      renderer.material.SetColor("_Color", color);
      Destroy(gameObject);
      }
    }

  private void Start()
    {
    GetComponent<Renderer>().material.SetColor("_Color", Color);
    StartCoroutine(DeleteAfterTime(5f));
    }

  IEnumerator DeleteAfterTime(float delay)
    {
    yield return new WaitForSeconds(delay);
    Destroy(gameObject);
    }
  }
