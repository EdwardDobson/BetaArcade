using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
public class ScoreManager : MonoBehaviour
{
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
    [SerializeField]
    bool startGame;
    bool gameStarted = false;
    public  float pointDrainTimer = 1;
    // Start is called before the first frame update
    void Start()
    {
        PlayerUI = GameObject.Find("PlayerUI").transform.GetChild(1).gameObject;
        point = GetComponent<PointMove>();
        roundText = GameObject.Find("roundText").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("timerText").GetComponent<TextMeshProUGUI>();
        winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        scoreToWinText = GameObject.Find("ScoreToWinText").GetComponent<TextMeshProUGUI>();
        inPointText = GameObject.Find("inPointText").GetComponent<TextMeshProUGUI>();
        scoreToWinTextTutorialText = GameObject.Find("ScoreToWin").GetComponent<TextMeshProUGUI>();
        timerTextTutorialText = GameObject.Find("RoundTimerText").GetComponent<TextMeshProUGUI>();
        roundTimerSlider = GameObject.Find("RoundTimeSlider").GetComponent<Slider>();
        scoreTimerSlider = GameObject.Find("ScoreToWinSlider").GetComponent<Slider>();
        scoreIncrease = GameObject.Find("Points").GetComponent<AudioSource>();
        KOTHPlayerSpawner = GetComponent<KOTHPlayerSpawner>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxRound = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
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
                    child.gameObject.SetActive(true);
                    if(child.CompareTag("Player" + child.GetComponent<PlayerMove>().ID))
                    {
                        child.gameObject.transform.position = KOTHPlayerSpawner.SpawnPoints[child.GetComponent<PlayerMove>().ID-1].position;
                    }

                    child.gameObject.GetComponent<PlayerMove>().hasDashed = false;
                    child.gameObject.GetComponent<PlayerMove>().MassPowerUpReset();
                    child.gameObject.GetComponent<PlayerMove>().hasPushed = false;
                    child.gameObject.GetComponent<PlayerMove>().dashSlider.value = child.gameObject.GetComponent<PlayerMove>().dashTimer;
                    child.gameObject.GetComponent<PlayerMove>().shoveSlider.value = child.gameObject.GetComponent<PlayerMove>().shoveTimer;
                    StartCoroutine(RespawnPlayer(child));
                    foreach (Transform t in PlayerUI.transform)
                    {
                        t.GetChild(0).GetComponent<Slider>().value = t.GetChild(0).GetComponent<Slider>().maxValue;
                        t.GetChild(7).GetComponent<Slider>().value = t.GetChild(7).GetComponent<Slider>().maxValue;
                    }
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
        _child.gameObject.GetComponent<Rigidbody>().mass = 300;
        yield return new WaitForSeconds(2);
        _child.gameObject.GetComponent<Rigidbody>().mass = 1;
    }
    void AddScoreOutOfTime()
    {
        if (gameManager.GetTimer() <= 0)
        {
            ResetScorePlayers();
            currentRound++;
            roundText.text = "Round: " + currentRound + " of " + maxRound;
            gameManager.SetTimer(10);
            resetPoints = true;
            ResetPoints();
        }
    }
    void ResetScorePlayers()
    {
        List<int> scores = new List<int>();
        foreach (var player in otherPlayers)
        {
            scores.Add(player.GetComponent<PointCollide>().GetScore());
            int temp = scores.Max();
            if (player.GetComponent<PointCollide>().GetScore() == temp && player.GetComponent<PointCollide>().GetScore() > 0)
            {
                switch (player.GetComponent<PlayerMove>().ID)
                {
                    case 1:
                        gameManager.SetPlayerOneScore(1);
                        break;
                    case 2:
                        gameManager.SetPlayerTwoScore(1);
                        break;
                    case 3:
                        gameManager.SetPlayerThreeScore(1);
                        break;
                    case 4:
                        gameManager.SetPlayerFourScore(1);
                        break;
                    default:

                        break;
                }
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
                    transform.GetChild(0).GetComponent<ChangeColour>().ModColour(otherPlayers[i].GetComponent<PlayerMove>().ID);
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
                transform.GetChild(0).GetComponent<ChangeColour>().ModColour(5);
            }
            if (otherPlayers[i].GetComponent<PointCollide>().GetScore() >= maxScore)
            {
                gameManager.SetPlayerScore(otherPlayers[i].GetComponent<PlayerMove>().ID,1);
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
        inPointCount = 0;
        yield return new WaitForSeconds(3);
        point.ResetTimer();
        canGainPoints = true;
        winText.text = "";
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            otherPlayers[i].GetComponent<Rigidbody>().mass = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
       
        if(other.tag.Contains("Player"))
        {
            inPointCount--;
            inPointText.text = "";
        }
        if (other.tag.Contains("Player"))
        {
            point.gameObject.GetComponent<MeshRenderer>().material = pointMat;
            transform.GetChild(0).GetComponent<ChangeColour>().ModColour(5);
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            inPointCount++;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            if(inPointCount == 1)
            {
                pointDrainTimer -= Time.deltaTime;
                if(pointDrainTimer <= 0)
                {
                    GetComponent<PointMove>().scoreAmountGive--;
                    pointDrainTimer = 1;
                    if (GetComponent<PointMove>().scoreAmountGive <= 0)
                    {
                        GetComponent<PointMove>().ForceMove();
                    }
                }
            }
         
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
    public void ChangeScoreToWin()
    {
        maxScore = (int)scoreTimerSlider.value;
        scoreToWinTextTutorialText.text = "Score to win : " + maxScore;
        scoreToWinText.text = "Score to win : " + maxScore;
    }
    public void ChangeRoundTime()
    {
        gameManager.SetTimer((int)roundTimerSlider.value);
        timerTextTutorialText.text = "Round Time : " + gameManager.GetTimer();
    }

 
}
