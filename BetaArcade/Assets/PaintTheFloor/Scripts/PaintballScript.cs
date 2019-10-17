using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballScript : MonoBehaviour
  {
  public Color Color;
  private void OnCollisionEnter(Collision collision)
    {
    var color = gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor");

    var renderer = collision.gameObject.GetComponent<Renderer>();
    if(renderer != null)
      {
      renderer.material.SetColor("_BaseColor", color);
      Destroy(gameObject);
      }
    }

  private void Start()
    {
    GetComponent<Renderer>().material.SetColor("_BaseColor", Color);
    StartCoroutine(DeleteAfterTime(5f));
    }

  IEnumerator DeleteAfterTime(float delay)
    {
    yield return new WaitForSeconds(delay);
    Destroy(gameObject);
    }
  }
