using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAMLevelManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject MolePrefab;

    private int m_PlayerCount = 0;
    private List<GameObject> m_SpawnPoints = new List<GameObject>();
    private int m_MaxMoles = 5;
    private int m_MoleCount = 0;

    private void Start()
    {
        var spawnPointsParent = GameObject.Find("SpawnPoints");
        foreach(Transform spawnPoint in spawnPointsParent.transform)
        {
            m_SpawnPoints.Add(spawnPoint.gameObject);
        }
        CreatePlayer();
        Physics.IgnoreLayerCollision(11, 12);
    }

    private void FixedUpdate()
    {
        if (m_MoleCount < m_MaxMoles)
        {
            CreateMole();
        }
    }

    private void CreatePlayer()
    {
        var player = Instantiate(PlayerPrefab);
        m_PlayerCount++;
        player.tag = "Player" + m_PlayerCount;
        player.GetComponent<PlayerMove>().ID = m_PlayerCount;
        player.transform.position = m_SpawnPoints[m_PlayerCount - 1].transform.position; // new Vector3(0, 1, 0); // TODO set position different for different players (maybe have spawn points)
    }
    private void CreateMole()
    {
        System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
        var x = rand.Next(0, 20) - 10;
        var z = rand.Next(0, 20) - 10;
        var mole = Instantiate(MolePrefab);
        mole.name = "Mole";
        mole.transform.position = new Vector3(x, -0.5f, z);
        m_MoleCount++;
    }
}
