using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    GameObject[] players = GameObject.FindObjectsOfType(typeof(GameObject)).Where(x => (x as GameObject).tag.Contains("Player")).Select(x => x as GameObject).ToArray();
    if (players.Length == 1)
      {
      players.ElementAt(0).GetComponent<PlayerManager>().SetWinner();
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
