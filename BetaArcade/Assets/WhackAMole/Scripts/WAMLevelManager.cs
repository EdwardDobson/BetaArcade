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

  private System.Random m_Rand = new System.Random(System.DateTime.Now.Millisecond);

  protected override void Start()
    {
    base.Start();
    var spawnPointsParent = GameObject.Find("SpawnPoints");
    foreach (Transform spawnPoint in spawnPointsParent.transform)
      m_SpawnPoints.Add(spawnPoint.gameObject);
    TargetPlayers = 1;

    CountdownTimer.Instance.Run();
    m_IsPaused = true;

    Physics.IgnoreLayerCollision(11, 12);
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

  protected override void CreatePlayer()
    {
    var player = Instantiate(PlayerPrefab);
    m_Players.Add(player);
    player.tag = "Player" + m_Players.Count;
    player.GetComponent<PlayerMove>().ID = m_Players.Count;
    player.transform.position = m_SpawnPoints[m_Players.Count - 1].transform.position;
    }
  public override IEnumerator EndRound()
    {
    yield return new WaitForSeconds(2);
    LevelCheck();
    m_CurrentRound++;
    }
  private void CreateMole()
    {
    var x = m_Rand.Next(0, 18) - 9;
    var z = m_Rand.Next(0, 18) - 9;
    var mole = Instantiate(MolePrefab);
    mole.name = "Mole";
    mole.transform.position = new Vector3(x, -0.5f, z);
    MoleCount++;
    }

  private IEnumerator SpawnDelay()
    {
    m_CanSpawnMole = false;
    yield return new WaitForSeconds(m_Rand.Next(20, 200) / 100.0f);
    m_CanSpawnMole = true;
    }
  }
