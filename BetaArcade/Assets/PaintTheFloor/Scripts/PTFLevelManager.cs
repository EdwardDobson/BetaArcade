using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTFLevelManager : MonoBehaviour
  {
  public GameObject Player;
  public GameObject FlatFloor;
  private int maxX = 50;
  private int maxY = 50;
  private int playerCount = 0;
  void Start()
    {
    FlatFloor.transform.localScale = new Vector3(maxX, .1f, maxY);
    Physics.IgnoreLayerCollision(9, 10);

    #region Level Generation
    for (int i = 0; i <= maxX; i++)
      {
      for (int y = 0; y <= maxY; y++)
        {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(i - (maxX / 2), 0, y - (maxY / 2));
        cube.transform.localScale = new Vector3(1, 0.2f, 1);
        }
      }
    #endregion
    CreatePlayer();
    }

  private void Update()
    {
    // DEBUG
    if (Debug.isDebugBuild)
      {
      if (Input.GetKeyDown(KeyCode.P) && playerCount < 4)
        CreatePlayer();
      }
    }

  private void CreatePlayer()
    {
    var player = Instantiate(Player);
    player.transform.position = new Vector3(5 * playerCount, .8f, 0);
    playerCount++;
    player.GetComponent<Renderer>().material.SetColor("_BaseColor", PlayerIDToColor(playerCount));
    player.GetComponent<PlayerMove>().ID = playerCount;
    }

  private Color PlayerIDToColor(int id)
    {
    switch (id)
      {
      case 1:
        return Color.red;
      case 2:
        return Color.yellow;
      case 3:
        return Color.green;
      case 4:
        return Color.blue;
      default:
        Debug.LogError("Player has no ID");
        break;
      }
    return Color.clear;
    }
  }
