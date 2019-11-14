using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTFLevelManager : KamilLevelManager
  {
  public GameObject Player;
  public GameObject FlatFloor;

  private int maxX = 50;
  private int maxY = 50;

  private bool m_RoundEnded = false;

  protected override void Start()
    {
    base.Start();
    FlatFloor.transform.localScale = new Vector3(maxX, .1f, maxY);
    Physics.IgnoreLayerCollision(9, 10);

    #region Level Generation
    LevelGeneration();
    #endregion

    CountdownTimer.Instance.Run();
    m_IsPaused = true;
    m_CurrentRound++;
    }

  private void Update()
    {
    if (!m_RoundEnded)
      {
      if (!m_IsPaused)
        {
        m_Timer -= Time.deltaTime;
        if (m_Timer <= 0)
          {
          StartCoroutine(EndRound());
          }
        }
      else
        {
        if (CountdownTimer.Instance.Timeleft <= 0)
          m_IsPaused = false;
        }
      }
    }

  protected override void CreatePlayer()
    {
    var player = Instantiate(Player);
    player.transform.position = new Vector3(5 * m_Players.Count, .8f, 0);
    m_Players.Add(player);
    player.tag = "Player" + m_Players.Count;
    player.GetComponent<Renderer>().material.SetColor("_BaseColor", LevelManagerTools.PlayerIDToColor(m_Players.Count));
    player.GetComponent<PlayerMove>().ID = m_Players.Count;
    }

  public override IEnumerator EndRound()
    {
    m_IsPaused = true;
    m_RoundEnded = true;
    // Add 1 score to players who won
    m_Players.Where(x => x.GetComponent<PTFMovement>().Score == m_Players.Max(p => p.GetComponent<PTFMovement>().Score))
             .ToList().ForEach(x => { if (m_GameManager != null) m_GameManager.SetPlayerScore(LevelManagerTools.GetPlayerID(x), 1); });
    yield return new WaitForSeconds(2);
    if (!LevelCheck())
      {
      m_CurrentRound++;
      var playerCount = m_Players.Count;
      foreach(var player in m_Players.Where(x => x != null))
        {
        Destroy(player);
        }
      m_Players = new List<GameObject>();
      LevelGeneration();
      TargetPlayers = playerCount;
      Debug.Log("Should spawn " + playerCount);
      CountdownTimer.Instance.Run();
      m_Timer = m_OldTimer;
      m_RoundEnded = false;
      }
    }

  private void LevelGeneration()
    {
    if (GameObject.Find("levelParent") != null)
      Destroy(GameObject.Find("levelParent"));
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
    }
  }
