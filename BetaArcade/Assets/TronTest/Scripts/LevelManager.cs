using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : KamilLevelManager
  {
  public GameObject PlayerObject;

  private bool m_RoundEnded = false;

  // Start is called before the first frame update
  protected override void Start()
    {
    base.Start();
    CountdownTimer.Instance.Run();
    m_IsPaused = true;
    m_CurrentRound++;
    }

  private void Update()
    {
    if(!m_RoundEnded)
      {
      if (m_Players.Where(x => x != null).Count() == 1)
        {
        var player = m_Players.FirstOrDefault(x => x != null);
        // Add 2 to player score for winning
        if (m_GameManager != null)
          m_GameManager.SetPlayerScore(player.GetComponent<PlayerManager>().ID, 2);
        player.GetComponent<PlayerManager>().IsDead = true;
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
    yield return new WaitForSeconds(1);
    LevelCheck();
    yield return new WaitForSeconds(1);
    m_CurrentRound++;
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

  protected override void CreatePlayer()
    {
    var player = Instantiate(PlayerObject);
    var playerScript = player.GetComponent<PlayerManager>();
    m_Players.Add(player);
    player.transform.position = new Vector3((10 * m_Players.Count) - 15, 1);
    player.tag = "Player" + m_Players.Count;
    playerScript.ID = m_Players.Count;
    player.GetComponent<Renderer>().material.SetColor("_BaseColor", LevelManagerTools.PlayerIDToColor(m_Players.Count));
    }
  }
