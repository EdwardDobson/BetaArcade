using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spleefGameManager : MonoBehaviour
{

    public GameObject[] players;
    public bool finished;
    public GameObject winner;
    // Start is called before the first frame update
    public GameObject PlayerPrefab;
    public int kills;
    public int total;
    public GameObject SpawnPointParent;
    private List<GameObject> SpawnPoints = new List<GameObject>(); //list

    private int PlayerCount = 0;
    GameManager gameManager;

    void Start()
    {
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        total = gameManager.GetPlayerCount() - 1;
        foreach (Transform SP in SpawnPointParent.transform)
        {
            SpawnPoints.Add(SP.gameObject);
        }

        for (int i = 0; i < gameManager.GetPlayerCount(); i++)
        {
            PlayerCreate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        kills = GameObject.Find("KillZone").GetComponent<killZone>().kills;

        if (kills > total)
        {
            finished = true;
        }
    }

    void PlayerCreate()
    {
        var player = GameObject.Instantiate(PlayerPrefab);
        player.transform.position = SpawnPoints[PlayerCount].transform.position;
        player.transform.SetParent(transform);
        var Movescript = player.GetComponent<PlayerMove>();
        PlayerCount++;
        Movescript.ID = PlayerCount;
    }
}

