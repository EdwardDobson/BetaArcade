using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KOTHPlayerSpawner : MonoBehaviour
{
    public GameObject Player;
    [SerializeField]
    public List<Transform> SpawnPoints = new List<Transform>();
    ScoreManager scoreManager;
    Transform playerHolder;
    private int playerCount = 0;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
       
            playerHolder = GameObject.Find("PlayerHolder").transform;
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            scoreManager = GetComponent<ScoreManager>();

        Invoke("LateStart", 0.1f);
    

    }
    private void LateStart()
    {
        if (playerCount < gameManager.GetPlayerCount())
        {
            for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
            {
                CreatePlayer();

            }


        }
    }
        // Update is called once per frame
    void Update()
    {
        
    }
    public void CreatePlayer()
    {
        GameObject player = Instantiate(Player);
   
        player.transform.position = SpawnPoints[playerCount].position;
        playerCount++;
        player.tag = "Player"+playerCount;
        player.GetComponent<Renderer>().material.SetColor("_BaseColor", PlayerIDToColor(playerCount));
        player.GetComponent<PlayerMove>().ID = playerCount;
        player.transform.SetParent(playerHolder);
        scoreManager.otherPlayers.Add(player);
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
    public Transform GetPlayerHolderTransform()
    {
        return playerHolder;
    }
}
