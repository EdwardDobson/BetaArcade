using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBTLevManager : KamilLevelManager
  {
  public GameObject PlayerPrefab;
  public GameObject SpawnPointParent;
  private List<GameObject> SpawnPoints = new List<GameObject>(); //list

  private bool m_RoundEnded = false;
  protected override void Start()
    {
    foreach (Transform SP in SpawnPointParent.transform)
      {
      SpawnPoints.Add(SP.gameObject);
      }
    base.Start();
    m_CurrentRound++;
    CountdownTimer.Instance.Run();
    m_IsPaused = true;
    }

  // Update is called once per frame
  void Update()
    {
    if (!m_RoundEnded)
      {
      if (!m_IsPaused)
        {
        m_Timer -= Time.deltaTime;
        if (m_Timer <= 0.0f)
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
    var player = GameObject.Instantiate(PlayerPrefab);
    player.transform.position = SpawnPoints[m_Players.Count].transform.position;
    player.transform.SetParent(transform);
    var Movescript = player.GetComponent<PlayerMove>();
    m_Players.Add(player);
    player.tag = "Player" + m_Players.Count;
    Movescript.ID = m_Players.Count;
    player.GetComponent<Renderer>().material.SetColor("_BaseColor", LevelManagerTools.PlayerIDToColor(m_Players.Count));
    }

  public override IEnumerator EndRound()
    {
    m_RoundEnded = true;
    // TODO Give +1 to players who won
   
    yield return new WaitForSeconds(2);
    if (!LevelCheck())
      {
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
    }
  }
