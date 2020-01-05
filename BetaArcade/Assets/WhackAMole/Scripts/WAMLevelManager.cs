using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAMLevelManager : KamilLevelManager
  {
  public GameObject PlayerPrefab;
  public GameObject MolePrefab;
  public int MoleCount = 0;

  private List<GameObject> m_SpawnPoints = new List<GameObject>();
  private int m_MaxMoles = 5;
  private bool m_CanSpawnMole = true;
  private bool m_RoundEnded = false;

  private System.Random m_Rand = new System.Random(System.DateTime.Now.Millisecond);

  protected override void Start()
    {
    m_RoundEnded = true;
    m_IsPaused = true;
    // Get spawn points
    var spawnPointsParent = GameObject.Find("SpawnPoints");
    foreach (Transform spawnPoint in spawnPointsParent.transform)
      m_SpawnPoints.Add(spawnPoint.gameObject);
    //StartGame();
    }

  public void StartGame()
    {
    // Create players
    base.Start();

    CountdownTimer.Instance.Run();
    m_IsPaused = true;
    m_RoundEnded = false;

    Physics.IgnoreLayerCollision(11, 12);
    CurrentRound++;
    }

  private void FixedUpdate()
    {
    if (!m_IsPaused && MoleCount < m_MaxMoles && m_CanSpawnMole)
      {
      CreateMole();
      StartCoroutine(SpawnDelay());
      }
    }

  private void Update()
    {
    if (!m_RoundEnded)
      {
      if (!m_IsPaused)
        {
        HandleTimer();
        }
      else
        {
        if (CountdownTimer.Instance.Timeleft <= 0)
          {
          m_IsPaused = false;
          m_Players.Where(x => x != null).Select(x => x.GetComponent<PlayerMove>()).ToList().ForEach(x => x.canMove = true);
          }
        }
      }
    }

  protected override void CreatePlayer()
    {
    var player = Instantiate(PlayerPrefab);
    m_Players.Add(player);
    player.tag = "Player" + m_Players.Count;
    player.GetComponent<PlayerMove>().ID = m_Players.Count;
    player.transform.position = m_SpawnPoints[m_Players.Count - 1].transform.position;
    LevelManagerTools.SetPlayerColor(player);
    //player.GetComponent<Renderer>().material.SetColor("_BaseColor", LevelManagerTools.PlayerIDToColor(m_Players.Count));
    }
  public override IEnumerator EndRound()
    {
    if (!m_RoundEnded)
      {
      Debug.Log("Round ending!");
      m_RoundEnded = true;
      // Give +1 to players who won
      m_Players.Where(x => x.GetComponent<WAMPlayerManager>().Score == m_Players.Max(p => p.GetComponent<WAMPlayerManager>().Score))
               .ToList().ForEach(x => { if (m_GameManager != null) m_GameManager.SetPlayerScore(LevelManagerTools.GetPlayerID(x), 1); });

      yield return new WaitForSeconds(.5f);
      if (!LevelCheck())
        {
        CurrentRound++;
        var playerCount = m_Players.Count;
        foreach (var player in m_Players.Where(x => x != null))
          {
          Destroy(player);
          }
        m_IsPaused = true;
        m_Players = new List<GameObject>();
        TargetPlayers = playerCount;
        foreach (var mole in GameObject.FindObjectsOfType<GameObject>().Where(x => x.name == "Mole"))
          {
          Destroy(mole);
          }
        MoleCount = 0;
        m_CanSpawnMole = true;
        CountdownTimer.Instance.Run();
        m_Timer = m_OldTimer;
        m_UITextScript.SetTimerText((int)m_Timer);
        m_RoundEnded = false;
        }
      }
    }
  private void CreateMole()
    {
    var x = m_Rand.Next(0, 18) - 9;
    var z = m_Rand.Next(0, 18) - 9;
    var mole = Instantiate(MolePrefab);
    mole.name = "Mole";
    mole.transform.position = new Vector3(x, -0.75f, z);
    MoleCount++;
    }

  private IEnumerator SpawnDelay()
    {
    m_CanSpawnMole = false;
    yield return new WaitForSeconds(m_Rand.Next(20, 200) / 100.0f);
    m_CanSpawnMole = true;
    }
  }
