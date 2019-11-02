using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
  {
  public GameObject PlayerObject;

  private int playerCount = 0;

  // Start is called before the first frame update
  void Start()
    {
    CreatePlayer();
    CreatePlayer();
    CreatePlayer();
    CreatePlayer();
    }

  private void Update()
    {
    if (Debug.isDebugBuild)
      {
      if (Input.GetKeyDown(KeyCode.P))
        {
        CreatePlayer();
        }
      }
    }

  void CreatePlayer()
    {
    var player = Instantiate(PlayerObject);
    var playerScript = player.GetComponent<PlayerManager>();
    player.transform.position = new Vector3((10 * playerCount++) - 15, 1);
    playerScript.ID = playerCount;
    player.GetComponent<Renderer>().material.SetColor("_BaseColor", LevelManagerTools.PlayerIDToColor(playerCount));
    }
  }
