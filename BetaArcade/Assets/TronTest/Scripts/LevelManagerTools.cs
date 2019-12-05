using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelManagerTools
  {
  public static Color PlayerIDToColor(int id)
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
  public static int GetPlayerID(GameObject playerObject)
    {
    if (playerObject != null)
      {
      if (playerObject.GetComponent<PlayerMove>() != null)
        {
        return playerObject.GetComponent<PlayerMove>().ID;
        }
      else if (playerObject.GetComponent<PlayerManager>() != null)
        {
        return playerObject.GetComponent<PlayerManager>().ID;
        }
      return -1;
      }
    return -1;
    }

  /// <summary>
  /// Returns the number of players should be in the scene whilst setting level variables
  /// </summary>
  /// <param name="maxRounds">Maximum number of rounds for this gamemode</param>
  /// <returns></returns>
  public static int GetLevelInfo(out int maxRounds)
    {
    maxRounds = 1;
    var gameManager = GameObject.Find("GameManager") != null ? GameObject.Find("GameManager").GetComponent<GameManager>() : null;
    if (gameManager != null)
      {
      maxRounds = gameManager.GetNumberOfRounds();
      return gameManager.GetPlayerCount();
      }
    return 1;
    }

  public static bool SetPlayerColor(GameObject playerObject, int id = -1)
    {
    if(id == -1)
      {
      id = GetPlayerID(playerObject);
      if (id == -1)
        {
        Debug.Log("Cannot get id for colour");
        return false;
        }
      }
    var characterObj = playerObject.transform.Find("character");
    if(characterObj == null)
      {
      Debug.LogWarning("Cannot get character object");
      return false;
      }
    var hairObj = characterObj.transform.Find("Cube.001");
    if (hairObj == null)
      {
      Debug.LogWarning("Cannot get hair object");
      return false;
      }
    var chestObj = characterObj.transform.Find("Cube.002");
    if(chestObj == null)
      {
      Debug.LogWarning("Cannot get chest object");
      return false;
      }
    var color = PlayerIDToColor(id);
    hairObj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", color);
    chestObj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", color);
    Debug.Log("Color set!");
    return true;
    }
  }
