using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
public class Win_Condition : MonoBehaviour
{
    public enum WinConditionType
    {
        eLastManStanding,
        eMostPoints,
        eTeamDeathmatch,
        eNull
    }

    public WinConditionType WinConType;

    TextMeshProUGUI winText;
    TextMeshProUGUI inPointText;
    TextMeshProUGUI roundText;
    TextMeshProUGUI timerText;
    TextMeshProUGUI scoreToWinText;
    TextMeshProUGUI scoreToWinTextTutorialText;
    TextMeshProUGUI timerTextTutorialText;

    Slider roundTimerSlider;
    Slider scoreTimerSlider;

    public int maxScore;

    bool resetPoints = false;
    int resetPointsCounter;

    bool canGainPoints = true;
    int scoreIncreaseValue = 1;

    [SerializeField]
    float timer;

    AudioSource scoreIncrease;

    Dodgeball_PlayerSpawner DodgballPlayerSpawner;

    Ball B;

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
    AudioSource scoreIncreaseClip;
    
    // Start is called before the first frame update
    void Start()
    {
        roundText = GameObject.Find("roundText").GetComponent<TextMeshProUGUI>();
        scoreIncreaseClip = GetComponent<AudioSource>();
        winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        inPointText = GameObject.Find("inPointText").GetComponent<TextMeshProUGUI>();
        DodgballPlayerSpawner = GetComponent<Dodgeball_PlayerSpawner>();
        maxRound = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
        B = GameObject.Find("Ball").GetComponent<Ball>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        roundText.text = "Round: 1 of " + maxRound;
        scoreTimerSlider = GameObject.Find("TutorialScreen").transform.GetChild(0).GetChild(6).GetComponent<Slider>();
        roundTimerSlider = GameObject.Find("TutorialScreen").transform.GetChild(0).GetChild(7).GetComponent<Slider>();
        timerTextTutorialText = GameObject.Find("TutorialScreen").transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>();
        scoreToWinTextTutorialText = GameObject.Find("TutorialScreen").transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>();
        scoreToWinText = GameObject.Find("Canvas").transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("Canvas").transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        timerTextTutorialText.text = "Round Time : " + roundTimerSlider.value;
        scoreToWinTextTutorialText.text = "Score To Win : " + scoreTimerSlider.value;
        timerText.text = "Round Time : " + roundTimerSlider.value;
        scoreToWinText.text = "Score To Win : " + scoreTimerSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (B.PlayersDown >= gameManager.GetPlayerCount()-1)
        {
            AddScore();
            B.PlayersDown = 0;
        }
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            //otherPlayers[i].GetComponent<PointCollide>().SetScore(scoreIncreaseValue);
            //gameManager.PlayerUIs[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Score: " + otherPlayers[i].GetComponent<PointCollide>().GetScore();
            //    scoreIncreaseClip.Play();
           
            timer = 0;
        }
    }
    public void AddScore() //call in ball i.e if (otherplayers <=1 Win_condition.AddScore)
    {
        /*
        switch (WinConType)
        {
            case (WinConditionType.eLastManStanding):
                {*/

                    Debug.Log("Passed Check");
                    for (int i = 0; i < otherPlayers.Count; i++)
                    {
                        if (otherPlayers[i].activeSelf)
                        {
                            inPointText.text = otherPlayers[i].tag + " is the last alive";
                            if (otherPlayers[i].tag == "Player1")
                            {
                                playerOneInGameScore++;

                                gameManager.SetPlayerOneScore(1);
                                NextRound();
                            }
                            if (otherPlayers[i].tag == "Player2")
                            {
                                playerTwoInGameScore++;

                                gameManager.SetPlayerTwoScore(1);
                                NextRound();
                            }
                            if (otherPlayers[i].tag == "Player3")
                            {
                                playerThreeInGameScore++;
                                gameManager.SetPlayerThreeScore(1);
                                NextRound();
                            }
                            if (otherPlayers[i].tag == "Player4")
                            {
                                playerFourInGameScore++;
                                gameManager.SetPlayerFourScore(1);
                                NextRound();
                            }
                        }
                        else
                        {
                            Debug.Log("No points");
                            NextRound();
                        }
                    }
        /*}
    break;

}*/
    }

    void NextRound()
    {
        currentRound++;
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            int SpawnPoint = 0;
            otherPlayers[i].SetActive(true);
            otherPlayers[i].transform.position = DodgballPlayerSpawner.SpawnPoints[i].position;

            Debug.Log("Player " + i + " " + otherPlayers[i].transform.position);

            SpawnPoint++;
        }
    }

    public void ChangeScoreToWin()
    {
        maxScore = (int)scoreTimerSlider.value;
        scoreToWinTextTutorialText.text = "Score To Win : " + maxScore;
        scoreToWinText.text = "Score To Win : " + maxScore;
    }

    public void ChangeRoundTime()
    {
        gameManager.SetTimer((int)roundTimerSlider.value);
        timerTextTutorialText.text = "Round Time : " + gameManager.GetTimer();
        timerText.text = "Round Time : " + gameManager.GetTimer();
    }
}
