using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTestScript : MonoBehaviour
  {
  public GameObject PlayersParent;
    // Start is called before the first frame update
    void Start()
    {
    int i = 0;
    foreach(Transform player in PlayersParent.transform)
      {
      i++;
      LevelManagerTools.SetPlayerColor(player.gameObject, i);
      }
    }
  }
