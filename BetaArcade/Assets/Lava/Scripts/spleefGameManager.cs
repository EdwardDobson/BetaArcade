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
    public GameObject SpawnPointParent;
    private List<GameObject> SpawnPoints = new List<GameObject>(); //list

    private int PlayerCount = 0;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    }

    void PlayerCreate()
    {
        var player = GameObject.Instantiate(PlayerPrefab);
        player.transform.position = SpawnPoints[PlayerCount].transform.position;
        

        var Movescript = player.GetComponent<PlayerMove>();
        PlayerCount++;
        player.tag = "Player" + PlayerCount;
        LevelManagerTools.SetPlayerColor(player, PlayerCount);
        Movescript.ID = PlayerCount;
        player.transform.SetParent(transform);

    }
}

