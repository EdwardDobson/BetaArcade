using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAMLevelManager : MonoBehaviour
  {
  public GameObject PlayerPrefab;
  public GameObject MolePrefab;
  public int MoleCount = 0;
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

  private int m_PlayerCount = 0;
  private List<GameObject> m_SpawnPoints = new List<GameObject>();
  private int m_MaxMoles = 5;
  private bool m_CanSpawnMole = true;

  private System.Random m_Rand = new System.Random(System.DateTime.Now.Millisecond);

  private void Start()
    {
    var spawnPointsParent = GameObject.Find("SpawnPoints");
    foreach (Transform spawnPoint in spawnPointsParent.transform)
      {
      m_SpawnPoints.Add(spawnPoint.gameObject);
      }
    // TargetPlayers should be set from Game Controller
    TargetPlayers = 1;
    Physics.IgnoreLayerCollision(11, 12);
    }

  private void FixedUpdate()
    {
    if (MoleCount < m_MaxMoles && m_CanSpawnMole)
      {
      CreateMole();
      StartCoroutine(SpawnDelay());
      }
    }

  private void CreatePlayer()
    {
    var player = Instantiate(PlayerPrefab);
    m_PlayerCount++;
    player.tag = "Player" + m_PlayerCount;
    player.GetComponent<PlayerMove>().ID = m_PlayerCount;
    player.transform.position = m_SpawnPoints[m_PlayerCount - 1].transform.position;
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
