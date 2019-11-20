using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI winText;
    TextMeshProUGUI inPointText;
    TextMeshProUGUI roundText;
    TextMeshProUGUI timerText;
    TextMeshProUGUI scoreToWinText;
    TextMeshProUGUI scoreToWinTextTutorialText;
    TextMeshProUGUI timerTextTutorialText;
    public int maxScore;
    bool resetPoints = false;
    bool canGainPoints = false;
    int scoreIncreaseValue = 1;
    int resetPointsCounter;
    [SerializeField]
    float timerScore;
    [SerializeField]
    float timer;
    PointMove point;
    [SerializeField]
    public Material pointMat;
    AudioSource scoreIncrease;
    [SerializeField]
    int inPointCount;
    KOTHPlayerSpawner KOTHPlayerSpawner;
    [SerializeField]
    public List<GameObject> otherPlayers = new List<GameObject>();
    //temp values
    [SerializeField]
    int currentRound;
    [SerializeField]
    int maxRound = 0;
    GameManager gameManager;
    bool endGameMode = false;
    GameObject PlayerUI;
    int stopTimerDecrease = 0;
    [SerializeField]
    bool startGame;
    bool gameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        stopTimerDecrease = 1;
        PlayerUI = GameObject.Find("PlayerUI").transform.GetChild(1).gameObject;
        point = GetComponent<PointMove>();
        roundText = GameObject.Find("roundText").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("timerText").GetComponent<TextMeshProUGUI>();
        winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        scoreToWinText = GameObject.Find("ScoreToWinText").GetComponent<TextMeshProUGUI>();
        inPointText = GameObject.Find("inPointText").GetComponent<TextMeshProUGUI>();
        scoreToWinTextTutorialText = GameObject.Find("ScoreToWin").GetComponent<TextMeshProUGUI>();
        timerTextTutorialText = GameObject.Find("RoundTimerText").GetComponent<TextMeshProUGUI>();
        scoreIncrease = GameObject.Find("Points").GetComponent<AudioSource>();
        KOTHPlayerSpawner = GetComponent<KOTHPlayerSpawner>();
        maxRound = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        roundText.text = "Round: 1 of " + maxRound;
        currentRound++;
        timerText.text = "Time: " + gameManager.GetTimer();
        timerTextTutorialText.text =  "Round Time: " + gameManager.GetTimer();
        scoreToWinTextTutorialText.text = "Score to win: " + maxScore;
        scoreToWinText.text = "Score to win: " + maxScore;
        gameManager.SetOldTimer();
        foreach (Transform t in PlayerUI.transform)
        {
            t.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Score: 0";
        }
       
    }
    void StartTime()
    {

            CountdownTimer.Instance.Run();
            if (CountdownTimer.Instance.Timeleft <= 0)
            {
                canGainPoints = true;
                gameStarted = true;
            }

     
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame == true)
        {
            if(!gameStarted || !canGainPoints)
            {
                StartTime();
            }
          
            if (canGainPoints && currentRound <= maxRound && !endGameMode)
            {
                AddScore();
                AddScoreOutOfTime();
                DecreaseTimerKoth();
            }
            if (currentRound > maxRound)
            {
                inPointText.text = "";
                roundText.text = "";
                timerText.text = "";
                winText.text = "";
                endGameMode = true;
                scoreToWinText.text = "";
                gameManager.transform.GetChild(0).gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Next Level"));
                startGame = false;
            }
            foreach (Transform child in KOTHPlayerSpawner.GetPlayerHolderTransform())
            {
                if (!child.gameObject.activeSelf)
                {
                    StartCoroutine(RespawnPlayer(child));
                }
            }
      
        }

    }
    public void SetStartGame(bool _state)
    {
        startGame = _state;
        timerText.text  = "Time: " + gameManager.GetTimer();
    }
    public bool GetStartGame()
    {
        return startGame;
    }
    void DecreaseTimerKoth()
    {
        timerScore -= Time.deltaTime;
        if (timerScore <= 0)
        {
            gameManager.DecreaseTimer(1);
            timerText.text = "Time: " + gameManager.GetTimer();
            timerScore = 1;
        }

    }
    IEnumerator RespawnPlayer(Transform _child)
    {
        yield return new WaitForSeconds(3);
        _child.gameObject.SetActive(true);
        if (_child.tag == "Player1")
        {
            _child.gameObject.transform.position = KOTHPlayerSpawner.SpawnPoints[0].position;
        }
        if (_child.tag == "Player2")
        {
            _child.gameObject.transform.position = KOTHPlayerSpawner.SpawnPoints[1].position;
        }
        if (_child.tag == "Player3")
        {
            _child.gameObject.transform.position = KOTHPlayerSpawner.SpawnPoints[2].position;
        }
        if (_child.tag == "Player4")
        {
            _child.gameObject.transform.position = KOTHPlayerSpawner.SpawnPoints[3].position;
        }
    }
    void AddScoreOutOfTime()
    {
        if (gameManager.GetTimer() <= 0)
        {
            ResetScorePlayers(0, 1, 2, 3);
            ResetScorePlayers(1, 0, 2, 3);
            ResetScorePlayers(2, 1, 0, 3);
            ResetScorePlayers(3, 1, 2, 0);
            currentRound++;
            roundText.text = "Round: " + currentRound + " of " + maxRound;
            gameManager.SetTimer(10);
            resetPoints = true;
            ResetPoints();
            stopTimerDecrease = 0;
        }
    }
    void ResetScorePlayers(int _id, int _id2, int _id3, int _id4)
    {
        if (gameManager.GetPlayerCount() == 2)
        {
            if (otherPlayers[_id].GetComponent<PointCollide>().GetScore() > otherPlayers[_id2].GetComponent<PointCollide>().GetScore())
            {
                if (_id == 0)
                {
                    gameManager.SetPlayerOneScore(1);
                }
                if (_id == 1)
                {
                    gameManager.SetPlayerTwoScore(1);
                }
                if (_id == 2)
                {
                    gameManager.SetPlayerThreeScore(1);
                }
                if (_id == 3)
                {
                    gameManager.SetPlayerFourScore(1);
                }
                winText.text = otherPlayers[_id].tag + " wins the round";
            }
            if (otherPlayers[_id].GetComponent<PointCollide>().GetScore() == otherPlayers[_id2].GetComponent<PointCollide>().GetScore())
            {
                winText.text = "Draw ";
            }
        }
        if (gameManager.GetPlayerCount() == 3)
        {
            if (otherPlayers[_id].GetComponent<PointCollide>().GetScore() > otherPlayers[_id2].GetComponent<PointCollide>().GetScore() && otherPlayers[_id].GetComponent<PointCollide>().GetScore() > otherPlayers[_id3].GetComponent<PointCollide>().GetScore())
            {
                if (_id == 0)
                {
                    gameManager.SetPlayerOneScore(1);
                }
                if (_id == 1)
                {
                    gameManager.SetPlayerTwoScore(1);
                }
                if (_id == 2)
                {
                    gameManager.SetPlayerThreeScore(1);
                }
                if (_id == 3)
                {
                    gameManager.SetPlayerFourScore(1);
                }
                winText.text = otherPlayers[_id].tag + " wins the round";
            }
            if (otherPlayers[_id].GetComponent<PointCollide>().GetScore() == otherPlayers[_id2].GetComponent<PointCollide>().GetScore() && otherPlayers[_id].GetComponent<PointCollide>().GetScore() == otherPlayers[_id3].GetComponent<PointCollide>().GetScore())
            {
                winText.text = "Draw ";
            }
        }
        if (gameManager.GetPlayerCount() == 4)
        {
            if (otherPlayers[_id].GetComponent<PointCollide>().GetScore() > otherPlayers[_id2].GetComponent<PointCollide>().GetScore() && otherPlayers[_id].GetComponent<PointCollide>().GetScore() > otherPlayers[_id3].GetComponent<PointCollide>().GetScore() && otherPlayers[_id].GetComponent<PointCollide>().GetScore() > otherPlayers[_id4].GetComponent<PointCollide>().GetScore())
            {
                if (_id == 0)
                {
                    gameManager.SetPlayerOneScore(1);
                }
                if (_id == 1)
                {
                    gameManager.SetPlayerTwoScore(1);
                }
                if (_id == 2)
                {
                    gameManager.SetPlayerThreeScore(1);
                }
                if (_id == 3)
                {
                    gameManager.SetPlayerFourScore(1);
                }
                winText.text = otherPlayers[_id].tag + " wins the round";
            }
            if (otherPlayers[_id].GetComponent<PointCollide>().GetScore() == otherPlayers[_id2].GetComponent<PointCollide>().GetScore() && otherPlayers[_id].GetComponent<PointCollide>().GetScore() == otherPlayers[_id3].GetComponent<PointCollide>().GetScore() && otherPlayers[_id].GetComponent<PointCollide>().GetScore() == otherPlayers[_id4].GetComponent<PointCollide>().GetScore())
            {
                winText.text = "Draw ";
            }
        }

    }
    void AddScore()
    {
        for (int i = 0; i < otherPlayers.Count; ++i)//Used to check if any other player is in the zone
        {
            if (inPointCount <= 1 && otherPlayers[i].GetComponent<PointCollide>().GetScore() != maxScore)
            {
                if (otherPlayers[i].GetComponent<PointCollide>().GetinPoint() && !resetPoints)
                {
                    inPointText.text = "Player " + (i+1)  + " is on the point";
                    point.gameObject.GetComponent<MeshRenderer>().material = otherPlayers[i].GetComponent<PointCollide>().pointMat;
                    timer += Time.deltaTime;
                    if (timer >= 1)
                    {
                        otherPlayers[i].GetComponent<PointCollide>().SetScore(scoreIncreaseValue);
                        gameManager.PlayerPictures[i].transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Score: " + otherPlayers[i].GetComponent<PointCollide>().GetScore();
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
                }
                if (otherPlayers[i].tag == "Player2")
                {
                    gameManager.SetPlayerTwoScore(1);
                }
                if (otherPlayers[i].tag == "Player3")
                {
                    gameManager.SetPlayerThreeScore(1);
                }
                if (otherPlayers[i].tag == "Player4")
                {
                    gameManager.SetPlayerFourScore(1);
                }
                winText.text = "Player " + (i + 1) + " wins the round";
                currentRound++;
                roundText.text = "Round: " + currentRound + " of " + maxRound;

                resetPoints = true;
            }
        }
        ResetPoints();
    }
    void ResetPoints()
    {
        if (resetPoints == true)
        {
            for (int i = 0; i < otherPlayers.Count; i++)
            {
                otherPlayers[i].GetComponent<PointCollide>().ResetScore(0);
                gameManager.PlayerPictures[i].transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Score: 0";
                resetPointsCounter++;
                canGainPoints = false;
                otherPlayers[i].transform.position = KOTHPlayerSpawner.SpawnPoints[i].position;
                inPointText.text = "";
                foreach (Transform child in KOTHPlayerSpawner.GetPlayerHolderTransform())
                {
                    if (!otherPlayers[i].activeSelf)
                    {
                        otherPlayers[i].SetActive(true);
                    }
                }
            }
            if (resetPointsCounter >= otherPlayers.Count)
            {
                resetPoints = false;
                point.MovePoint();
                StartCoroutine(CanGainPoints());
                gameManager.SetTimer(gameManager.GetOldTimer());
            }
            point.SetResetPoints(true);
            point.ResetPoints();
        }
    }
    IEnumerator CanGainPoints()
    {
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            otherPlayers[i].GetComponent<Rigidbody>().mass = 200;
        }
        yield return new WaitForSeconds(3);
        point.ResetTimer();
        canGainPoints = true;
        winText.text = "";
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            otherPlayers[i].GetComponent<Rigidbody>().mass = 1;
        }
        stopTimerDecrease = 1;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
        {
            point.gameObject.GetComponent<MeshRenderer>().material = pointMat;
        }
        if (other.gameObject.tag == "Player1")
        {
            inPointCount--;
            inPointText.text = "";
        }
        if (other.gameObject.tag == "Player2")
        {
            inPointCount--;
            inPointText.text = "";
        }
        if (other.gameObject.tag == "Player3")
        {
            inPointCount--;
            inPointText.text = "";
        }
        if (other.gameObject.tag == "Player4")
        {
            inPointCount--;
            inPointText.text = "";
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            inPointCount++;
        }
        if (other.gameObject.tag == "Player2")
        {
            inPointCount++;
        }
        if (other.gameObject.tag == "Player3")
        {
            inPointCount++;
        }
        if (other.gameObject.tag == "Player4")
        {
            inPointCount++;
        }
    }
    public bool GetresetPoints()
    {
        return resetPoints;
    }
    public void SetresetPoints(bool _reset)
    {
        resetPoints = _reset;
    }
    public void IncreaseScoreToWin()
    {
        if (maxScore < 1000)
        {
            maxScore += 5;
            scoreToWinTextTutorialText.text = "Score to win: " + maxScore;
            scoreToWinText.text = "Score to win: " + maxScore;
        }

    }
    public void DecreaseScoreToWin()
    {
        if (maxScore > 5)
        {
            maxScore -= 5;
            scoreToWinTextTutorialText.text = "Score to win: " + maxScore;
            scoreToWinText.text = "Score to win: " + maxScore;
        }

    }
    public void IncreaseTimer()
    {
        gameManager.IncreaseTimer(5);
        timerTextTutorialText.text = "Round Time: " + gameManager.GetTimer();
    }
    public void DecreaseTimer()
    {
        if (gameManager.GetTimer() > 60)
        {
            gameManager.DecreaseTimer(5);
            timerTextTutorialText.text = "Round Time: " + gameManager.GetTimer();
        }
    
    }
}
