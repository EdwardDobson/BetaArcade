using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball_PlayerSpawner : MonoBehaviour
{
    public GameObject Player;
    [SerializeField]
    public List<Transform> SpawnPoints = new List<Transform>();
    Win_Condition WinCondition;

    public int playerCount = 0;
    GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        WinCondition = GetComponent<Win_Condition>();
        if (playerCount < gameManager.GetPlayerCount())
        {
            for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
            {
                CreatePlayer();
            }
        }
    }
    public void CreatePlayer()
    {
        GameObject player = Instantiate(Player);

        if (playerCount == 0)
        {
            player.tag = "Player1";
        }
        if (playerCount == 1)
        {
            player.tag = "Player2";
        }
        if (playerCount == 2)
        {
            player.tag = "Player3";
        }
        if (playerCount == 3)
        {
            player.tag = "Player4";
        }

        player.transform.position = SpawnPoints[playerCount].position;

        playerCount++;

        player.GetComponent<PlayerMove>().ID = playerCount;
        LevelManagerTools.SetPlayerColor(player);
        WinCondition.otherPlayers.Add(player);
    }
}
