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

    // Start is called before the first frame update
    void Awake()
    {
        WinCondition = GetComponent<Win_Condition>();
        if (playerCount < 4)
        {
            for (int i = 0; i < 4; ++i)
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

        player.GetComponent<Renderer>().material.SetColor("_BaseColor", PlayerIDToColor(playerCount));

        player.GetComponent<PlayerMove>().ID = playerCount;

        WinCondition.otherPlayers.Add(player);
    }
    public void DecreasePlayerCount()
    {
        playerCount--;
    }

    private Color PlayerIDToColor(int id)
    {
        switch (id)
        {
            case 1:
                return Color.red;
            case 2:
                return Color.yellow;
            case 3:
                return Color.green;
            case 4:
                return Color.blue;
            default:
                Debug.LogError("Player has no ID");
                break;
        }
        return Color.clear;
    }
}
