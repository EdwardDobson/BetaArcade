using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EndZoneScript : MonoBehaviour
{
    public GameObject Player;
    private int playerCount = 0;
    [SerializeField]
    List<Transform> spawnPoints = new List<Transform>();
    List<GameObject> players = new List<GameObject>();
    List<int> scores = new List<int>();
    GameManager gameManager;
    int highestScore;
    int secondHighest;
    int thirdHighest;
    int lowestScore;
    // Start is called before the first frame update
    void Start()
    {
      
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (playerCount < gameManager.GetPlayerCount())
        {
            for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
            {
                CreatePlayer();
            }

        }
        scores.Add(gameManager.GetPlayerOneScore());
        scores.Add(gameManager.GetPlayerTwoScore());
        scores.Add(gameManager.GetPlayerThreeScore());
        scores.Add(gameManager.GetPlayerFourScore());
        highestScore = Mathf.Max(scores.ToArray());
        lowestScore = Mathf.Min(scores.ToArray());
        secondHighest = (from number in scores orderby number descending select number).Skip(1).First();
        thirdHighest = (from number in scores orderby number descending select number).Skip(2).First();
        CompareScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CompareScores()
    {
        var highScores = new List<int>() { highestScore, secondHighest, thirdHighest, lowestScore };
        foreach(var player in players)
        {
            int playerScore = gameManager.GetPlayerScore(LevelManagerTools.GetPlayerID(player));
            player.transform.position = spawnPoints[highScores.IndexOf(playerScore)].position;
        }
        
        //if(highestScore == gameManager.GetPlayerOneScore())
        //{
        //    players[0].transform.position = spawnPoints[0].position;
        //}
        //if (highestScore == gameManager.GetPlayerTwoScore())
        //{
        //    players[1].transform.position = spawnPoints[0].position;
        //}
        //if (secondHighest == gameManager.GetPlayerOneScore())
        //{
        //    players[0].transform.position = spawnPoints[1].position;
        //}
        //if (secondHighest == gameManager.GetPlayerTwoScore())
        //{
        //    players[1].transform.position = spawnPoints[1].position;
        //}
        //if (thirdHighest == gameManager.GetPlayerOneScore())
        //{
        //    players[0].transform.position = spawnPoints[2].position;
        //}
        //if (thirdHighest == gameManager.GetPlayerTwoScore())
        //{
        //    players[1].transform.position = spawnPoints[2].position;
        //}
        //if (lowestScore == gameManager.GetPlayerOneScore())
        //{
        //    players[0].transform.position = spawnPoints[3].position;
        //}
        //if (lowestScore == gameManager.GetPlayerTwoScore())
        //{
        //    players[1].transform.position = spawnPoints[3].position;
        //}
        //if (gameManager.GetPlayerCount() >= 3)
        //{
        //    if (highestScore == gameManager.GetPlayerThreeScore())
        //    {
        //        players[2].transform.position = spawnPoints[0].position;
        //    }
        //    if (secondHighest == gameManager.GetPlayerThreeScore())
        //    {
        //        players[2].transform.position = spawnPoints[1].position;
        //    }
        //    if (thirdHighest == gameManager.GetPlayerThreeScore())
        //    {
        //        players[2].transform.position = spawnPoints[2].position;
        //    }
        //    if (lowestScore == gameManager.GetPlayerThreeScore())
        //    {
        //        players[2].transform.position = spawnPoints[3].position;
        //    }
        //}
        //if (gameManager.GetPlayerCount() >= 4)
        //{
        //    if (highestScore == gameManager.GetPlayerFourScore())
        //    {
        //        players[3].transform.position = spawnPoints[0].position;
        //    }
        //    if (secondHighest == gameManager.GetPlayerFourScore())
        //    {
        //        players[3].transform.position = spawnPoints[1].position;
        //    }
        //    if (thirdHighest == gameManager.GetPlayerFourScore())
        //    {
        //        players[3].transform.position = spawnPoints[2].position;
        //    }
        //    if (lowestScore == gameManager.GetPlayerFourScore())
        //    {
        //        players[3].transform.position = spawnPoints[3].position;
        //    }
        //}
   
      
     
    }
    public void CreatePlayer()
    {
        GameObject player = Instantiate(Player);
        player.transform.position = spawnPoints[playerCount].position;
        player.transform.SetParent(GameObject.Find("PlayerHolder").transform);
        playerCount++;
        player.tag = "Player" + playerCount;
        player.GetComponent<Renderer>().material.SetColor("_BaseColor", PlayerIDToColor(playerCount));
        player.GetComponent<PlayerMove>().ID = playerCount;
        players.Add(player);
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
