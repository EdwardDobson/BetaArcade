﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : KamilLevelManager
  {
  public GameObject PlayerObject;
  public GameObject SpawnPointsParent;
  private List<Transform> m_SpawnPoints = new List<Transform>();

  private bool m_RoundEnded = false;
  // Start is called before the first frame update
  protected override void Start()
    {
    m_RoundEnded = true;
    foreach (Transform child in SpawnPointsParent.transform)
      {
      m_SpawnPoints.Add(child.transform);
      }
    }

  public void StartGame()
    {
    base.Start();
    CountdownTimer.Instance.Run();
    m_IsPaused = true;
    m_RoundEnded = false;
    CurrentRound++;
    }

  private void Update()
    {
    if (!m_RoundEnded)
      {
      if (m_Players.Where(x => x != null).Count() <= 1)
        {
        var player = m_Players.FirstOrDefault(x => x != null);
        if (player != null)
          {
          if (m_GameManager != null)
            m_GameManager.SetPlayerScore(player.GetComponent<PlayerManager>().ID, 1);
          player.GetComponentInChildren<Animator>().speed = 1;
          player.GetComponentInChildren<Animator>().SetBool("HasWon", true);
          player.GetComponent<PlayerManager>().ToggleFreeze(true);
          }
        // Add 2 to player score for winning
        StartCoroutine(EndRound());
        }

      if (!m_IsPaused)
        {
        HandleTimer();
        }
      else
        {
        if (CountdownTimer.Instance.Timeleft <= 0)
          {
          m_IsPaused = false;
          }
        }
      }
    }

  public override IEnumerator EndRound()
    {
    m_RoundEnded = true;
    yield return new WaitForSeconds(2);
    if (!LevelCheck())
      {
      CurrentRound++;
      m_UITextScript.SetRoundText(CurrentRound, m_MaxRounds);
      var playerCount = m_Players.Count;
      foreach (var player in m_Players.Where(x => x != null))
        {
        Destroy(player);
        }
      m_IsPaused = true;
      m_Players = new List<GameObject>();
      TargetPlayers = playerCount;
      CountdownTimer.Instance.Run();
      m_Timer = m_OldTimer;
      m_RoundEnded = false;
      }
    }

  protected override void CreatePlayer()
    {
    var player = Instantiate(PlayerObject);
    var playerScript = player.GetComponent<PlayerManager>();
    m_Players.Add(player);
    //player.transform.position = new Vector3((10 * m_Players.Count) - 15, 1);
    player.transform.position = m_SpawnPoints[m_Players.Count() - 1].position;
    player.tag = "Player" + m_Players.Count;
    playerScript.ID = m_Players.Count;
    LevelManagerTools.SetPlayerColor(player, m_Players.Count);
    }
  }
