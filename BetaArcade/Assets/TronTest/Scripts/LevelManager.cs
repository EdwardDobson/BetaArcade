using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
  {
  public GameObject PlayerObject;

  private int m_PlayerCount = 0;
  private float m_Timer;
  private float m_MaxTime = 90f;
  private GameObject[] players;
  public int TargetPlayers
    {
    set
      {
      for(int i = m_PlayerCount; i < value; i++)
        {
        CreatePlayer();
        }
      }
    }

  // Start is called before the first frame update
  void Start()
    {
    m_Timer = m_MaxTime;
    TargetPlayers = 4;
    players = GameObject.FindObjectsOfType(typeof(GameObject)).Where(x => (x as GameObject).tag.Contains("Player")).Select(x => x as GameObject).ToArray();
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

    if(players.Where(x => x != null).Count() == 1)
      {
      players.First(x => x != null).GetComponent<PlayerManager>().SetWinner();
      }

    m_Timer -= Time.deltaTime;
    if(m_Timer <= 0.0f)
      {
      End();
      }
    }

  private void End()
    {
    // Loop through players and check who won
    }

  void CreatePlayer()
    {
    var player = Instantiate(PlayerObject);
    var playerScript = player.GetComponent<PlayerManager>();
    player.transform.position = new Vector3((10 * m_PlayerCount++) - 15, 1);
    playerScript.ID = m_PlayerCount;
    player.GetComponent<Renderer>().material.SetColor("_BaseColor", LevelManagerTools.PlayerIDToColor(m_PlayerCount));
    }
  }
