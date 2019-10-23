﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Win_Condition : MonoBehaviour
{
    public enum WinConditionType
    {
        eLastManStanding,
        eKOTH,
        eMostPoints,
        eTeamDeathmatch,
        eNull

    }

    public WinConditionType WinConType;

    TextMeshProUGUI winText;
    TextMeshProUGUI inPointText;
    TextMeshProUGUI roundText;

    public int maxScore;

    bool resetPoints = false;
    int resetPointsCounter;

    bool canGainPoints = true;
    int scoreIncreaseValue = 1;

    [SerializeField]
    float timer;
    PointMove point;

    [SerializeField]
    public Material pointMat;
    AudioSource scoreIncrease;

    [SerializeField]
    int inPointCount;
    Dodgeball_PlayerSpawner DodgballPlayerSpawner;

    [SerializeField]
    public List<GameObject> otherPlayers = new List<GameObject>();

    [SerializeField]
    int playerOneInGameScore;

    [SerializeField]
    int playerTwoInGameScore;

    [SerializeField]
    int playerThreeInGameScore;

    [SerializeField]
    int playerFourInGameScore;

    //temp values
    [SerializeField]
    int currentRound;

    [SerializeField]
    int maxRound = 0;

    GameManager gameManager;

    bool endGameMode = false;

    int tempPointCount;
    // Start is called before the first frame update
    void Start()
    {
        point = GetComponent<PointMove>();
        roundText = GameObject.Find("roundText").GetComponent<TextMeshProUGUI>();
        scoreIncrease = GameObject.Find("Points").GetComponent<AudioSource>();
        winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        inPointText = GameObject.Find("inPointText").GetComponent<TextMeshProUGUI>();
        DodgballPlayerSpawner = GetComponent<Dodgeball_PlayerSpawner>();
        maxRound = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        roundText.text = "Round: 1 of " + maxRound;
    }

    // Update is called once per frame
    void Update()
    {
        if (canGainPoints && currentRound <= maxRound && !endGameMode)
        {
            AddScore();
        }
        if (currentRound > maxRound)
        {
            inPointText.text = "";
            roundText.text = "";
            endGameMode = true;
        }
    }
    void AddScore()
    {
        switch (WinConType)
        {

            case (WinConditionType.eKOTH):
                {
                    for (int i = 0; i < otherPlayers.Count; ++i)//Used to check if any other player is in the zone
                    {
                        if (inPointCount <= 1 && otherPlayers[i].GetComponent<PointCollide>().GetScore() != maxScore)
                        {
                            if (otherPlayers[i].GetComponent<PointCollide>().GetinPoint() && !resetPoints)
                            {
                                inPointText.text = otherPlayers[i].tag + " is on the point";
                                point.gameObject.GetComponent<MeshRenderer>().material = otherPlayers[i].GetComponent<PointCollide>().pointMat;
                                timer += Time.deltaTime;
                                if (timer >= 1)
                                {
                                    otherPlayers[i].GetComponent<PointCollide>().SetScore(scoreIncreaseValue);
                                    gameManager.PlayerUIs[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Score: " + otherPlayers[i].GetComponent<PointCollide>().GetScore();
                                    scoreIncrease.Play();
                                    timer = 0;
                                }
                            }
                        }
                        else if (inPointCount >= 2)
                        {
                            point.gameObject.GetComponent<MeshRenderer>().material = pointMat;
                            inPointText.text = "Point is being contested";
                        }
                        if (otherPlayers[i].GetComponent<PointCollide>().GetScore() >= maxScore)
                        {
                            if (otherPlayers[i].tag == "Player1")
                            {
                                gameManager.SetPlayerOneScore(1);
                                playerOneInGameScore++;
                            }
                            if (otherPlayers[i].tag == "Player2")
                            {
                                gameManager.SetPlayerTwoScore(1);
                                playerTwoInGameScore++;
                            }
                            if (otherPlayers[i].tag == "Player3")
                            {
                                gameManager.SetPlayerThreeScore(1);
                                playerThreeInGameScore++;
                            }
                            if (otherPlayers[i].tag == "Player4")
                            {
                                gameManager.SetPlayerFourScore(1);
                                playerFourInGameScore++;
                            }
                            winText.text = otherPlayers[i].tag + " wins the round";
                            currentRound++;
                            currentRound++;
                            roundText.text = "Round: " + currentRound + " of " + maxRound;
                            resetPoints = true;
                        }
                    }
                }
            break;

        }

        if (resetPoints == true)
        {
            for (int i = 0; i < otherPlayers.Count; i++)
            {
                otherPlayers[i].GetComponent<PointCollide>().ResetScore(0);
                gameManager.PlayerUIs[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Score: 0";
                resetPointsCounter++;
                canGainPoints = false;
                otherPlayers[i].transform.position = DodgballPlayerSpawner.SpawnPoints[i].position;
                inPointText.text = "";
            }
            if (resetPointsCounter >= otherPlayers.Count)
            {
                resetPoints = false;
                point.MovePoint();
                StartCoroutine(CanGainPoints());
            }
        }
    }
    IEnumerator CanGainPoints()
    {
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            otherPlayers[i].GetComponent<Rigidbody>().mass = 200;
        }
        yield return new WaitForSeconds(2);
        canGainPoints = true;
        winText.text = "";
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            otherPlayers[i].GetComponent<Rigidbody>().mass = 1;
        }

    }
}
