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
  }
