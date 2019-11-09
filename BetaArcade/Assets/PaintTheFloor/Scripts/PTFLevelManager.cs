using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTFLevelManager : MonoBehaviour
  {
  public GameObject Player;
  public GameObject FlatFloor;
  public int TargetPlayers
    {
    set
      {
      for(int i = playerCount; i < value; i++)
        {
        CreatePlayer();
        }
      }
    }

  private int maxX = 50;
  private int maxY = 50;
  private int playerCount = 0;
  private float m_Timer;
  private float m_MaxTime = 90;

  void Start()
    {
    FlatFloor.transform.localScale = new Vector3(maxX, .1f, maxY);
    Physics.IgnoreLayerCollision(9, 10);

    #region Level Generation
    var levelParent = new GameObject("levelParent");
    for (int i = 0; i <= maxX; i++)
      {
      for (int y = 0; y <= maxY; y++)
        {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(i - (maxX / 2), 0, y - (maxY / 2));
        cube.transform.localScale = new Vector3(1, 0.2f, 1);
        cube.transform.SetParent(levelParent.transform);
        }
      }
    #endregion
    CreatePlayer();
    m_Timer = m_MaxTime;
    }

  private void Update()
    {
    // DEBUG
    if (Debug.isDebugBuild)
      {
      if (Input.GetKeyDown(KeyCode.P) && playerCount < 4)
        CreatePlayer();
      }

    m_Timer -= Time.deltaTime;
    if(m_Timer <= 0)
      {
      // TODO end
      }
    }

  private void CreatePlayer()
    {
    var player = Instantiate(Player);
    player.transform.position = new Vector3(5 * playerCount, .8f, 0);
    playerCount++;
    player.tag = "Player" + playerCount;
    player.GetComponent<Renderer>().material.SetColor("_BaseColor", LevelManagerTools.PlayerIDToColor(playerCount));
    player.GetComponent<PlayerMove>().ID = playerCount;
    }
  }
