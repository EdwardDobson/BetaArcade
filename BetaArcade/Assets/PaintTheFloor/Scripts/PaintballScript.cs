using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballScript : MonoBehaviour
  {
  public Color Color;
  private void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.tag == "PaintDelete")
      {
      Destroy(gameObject);
      }
    }
  private void OnTriggerEnter(Collider other)
    {
    var color = gameObject.GetComponent<Renderer>().material.GetColor("_Color");

    var renderer = other.gameObject.GetComponent<Renderer>();
    if (renderer != null && !other.name.Contains("PaintBall"))
      {
      IEnumerable<GameObject> players = GameObject.FindObjectsOfType<GameObject>().Where(x => x.tag.Contains("Player"));

      foreach (var player in players)
        {
        if (LevelManagerTools.GetPlayerMaterial(player).GetColor("_Color") == renderer.material.GetColor("_Color"))
          {
          player.GetComponent<PTFMovement>().Score--;
          }
        if (LevelManagerTools.GetPlayerMaterial(player).GetColor("_Color") == color)
          {
          player.GetComponent<PTFMovement>().Score++;
          Debug.Log("Adding score to: " + player.name);
          }
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
