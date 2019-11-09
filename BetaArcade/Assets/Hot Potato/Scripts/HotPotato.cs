using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class HotPotato : MonoBehaviour
{
    // Start is called before the first frame update
    #region Bomb
    public int maxBombTimer = 5;
    [SerializeField]
    int currentBombTimer;
    float timer = 1;
    #endregion
    #region PassBomb
    [SerializeField]
    public List<GameObject> players = new List<GameObject>();
    GameManager gameManager;
    #endregion
    [SerializeField]
    int increaseInactivePlayers;
    TextMeshProUGUI roundText;
    public TextMeshProUGUI bombTimerTitle;
    [SerializeField]
    int currentRound = 0;
    [SerializeField]
    int maxRound;
    bool endGameMode = false;
    bool startGame = false;
    TextMeshProUGUI bombTimerText;
    TextMeshProUGUI startTimeText;
    GameObject HotPotatoUI;
    float startTime = 4;
    bool gameStarted = false;
    bool canPassBomb = false;
    bool roundRestarting;
    void Start()
    {
        maxBombTimer = 5;
        Invoke("LateStart", 0.1f);
        bombTimerText = GameObject.Find("BombTimer").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startTimeText = GameObject.Find("StartTimeText").GetComponent<TextMeshProUGUI>();
        bombTimerTitle.text = "Bomb Timer: " + maxBombTimer;
    }
    void LateStart()
    {
        roundText = GameObject.Find("RoundText").GetComponent<TextMeshProUGUI>();
        HotPotatoUI = GameObject.Find("HotPotatoUI");
        currentBombTimer = maxBombTimer;

        currentRound++;
        maxRound = gameManager.GetNumberOfRounds();
        roundText.text = "Round: 1 of " + maxRound;
    }
    void StartTime()
    {
        startTime -= Time.deltaTime;
        startTimeText.text = "" + (int)startTime;
        if (startTime <= 1)
        {
            canPassBomb = true;
            startTimeText.text = "Go!";
            startTime = 4;
            gameStarted = true;
            StartCoroutine(HideStartTimeText());
            for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
            {
                int randomBombPick = Random.Range(0, gameManager.GetPlayerCount());
                if (!players[randomBombPick].GetComponent<PlayerHotPotato>().HasBomb())
                {
                    players[randomBombPick].GetComponent<PlayerHotPotato>().SetHasBomb(true);
                    break;
                }
            }
        }
    }
    IEnumerator HideStartTimeText()
    {
        yield return new WaitForSeconds(1);
        startTimeText.text = "";
    }

    void BombTimer()
    {
        if (!roundRestarting)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 1;
                currentBombTimer--;
                bombTimerText.text = "Bomb Timer: " + currentBombTimer;
            }
        }
      
    }
    // Update is called once per frame
    void Update()
    {
        if (startGame == true)
        {
            if (currentRound > maxRound)
            {
                endGameMode = true;
                roundText.text = "";
                currentRound = maxRound;
                HotPotatoUI.SetActive(false);
                gameManager.transform.GetChild(0).gameObject.SetActive(true);
                gameManager.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                gameManager.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                gameManager.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Next Level"));

            }
            if(!canPassBomb || !gameStarted)
            {
                StartTime();
            }
            if (!endGameMode && canPassBomb)
            {
                BombTimer();
                bombTimerText.text = "Bomb Timer: " + currentBombTimer;
                if (increaseInactivePlayers >= gameManager.GetPlayerCount() - 1)
                {
                    StartCoroutine(ResetRoundCoroutine());
                    for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
                    {
                        if (players[i].gameObject.activeSelf)
                        {
                            if (players[i].tag == "Player1")
                            {
                                gameManager.SetPlayerOneScore(1);
                            }
                            if (players[i].tag == "Player2")
                            {
                                gameManager.SetPlayerTwoScore(1);
                            }
                            if (players[i].tag == "Player3")
                            {
                                gameManager.SetPlayerThreeScore(1);
                            }
                            if (players[i].tag == "Player4")
                            {
                                gameManager.SetPlayerFourScore(1);
                            }
                            increaseInactivePlayers = 0;
                            currentRound++;
                        }
                    }
                }
                ResetBomb();
                for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
                {
                    if (players[i].GetComponent<PlayerHotPotato>().HasBomb())
                    {
                        gameManager.PlayerPictures[i].transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "has the bomb";
                      
                    }
                    if (!players[i].GetComponent<PlayerHotPotato>().HasBomb())
                    {
                        gameManager.PlayerPictures[i].transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "";
                    }

                }
            }
        }
    }
    void ResetRound()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
            t.gameObject.transform.position = GetComponent<HOTPotatoSpawner>().SpawnPoints[t.GetComponent<PlayerMove>().ID - 1].transform.position;
            roundText.text = "Round: " + currentRound + " of " + maxRound;
            currentBombTimer = maxBombTimer;
            t.GetComponent<PlayerHotPotato>().SetCanTakeBomb(true);
            t.GetComponent<PlayerHotPotato>().SetHasBomb(false);
        }
        increaseInactivePlayers = 0;
        roundRestarting = false;
    }
    IEnumerator ResetRoundCoroutine()
    {
        currentBombTimer = maxBombTimer;
        roundRestarting = true;
        canPassBomb = false;
        yield return new WaitForSeconds(3);
        ResetRound();
   
        yield return new WaitForSeconds(3);
        for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
        {
            if (players[i].gameObject.activeSelf)
            {
                if (!players[i].GetComponent<PlayerHotPotato>().HasBomb())
                {
                    Debug.Log("HAS BOMB");
                    players[i].GetComponent<PlayerHotPotato>().SetHasBomb(true);
                    break;
                }
            }
        }
        canPassBomb = true;
    }
    void ResetBomb()
    {
        if (currentBombTimer <= 0 && !roundRestarting)
        {
            for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
            {
                if (players[i].GetComponent<PlayerHotPotato>().HasBomb())
                {
                    players[i].GetComponent<PlayerHotPotato>().SetHasBomb(false);
                    players[i].gameObject.SetActive(false);
                    currentBombTimer = maxBombTimer;
                    StartCoroutine(SwitchBombTarget());
                    increaseInactivePlayers++;
                    break;
                }
            }
        }
    }
    IEnumerator SwitchBombTarget()
    {
        yield return new WaitForSeconds(1);
        SwitchPlayer();
    }
    void SwitchPlayer()
    {
        if(!roundRestarting)
        {
            for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
            {
                int randomIndex = Random.Range(0, players.Count);
                if (players[randomIndex].gameObject.activeSelf)
                {
                    if (!players[randomIndex].GetComponent<PlayerHotPotato>().HasBomb())
                    {
                        Debug.Log("HAS BOMB");
                        players[randomIndex].GetComponent<PlayerHotPotato>().SetHasBomb(true);
                        break;
                    }
                }
            }
        }
    

    }
    public void StartGame(bool _state)
    {
        startGame = _state;
    }
    public void IncreaseBombTimer(int _timer)
    {
        if (maxBombTimer < 1000)
        {
            maxBombTimer += _timer;
            bombTimerTitle.text = "Bomb Timer: " + maxBombTimer;
        }
    }
    public void DecreaseBombTimer(int _timer)
    {
        if (maxBombTimer > 5)
        {
            maxBombTimer -= _timer;
            bombTimerTitle.text = "Bomb Timer: " + maxBombTimer;
        }
    }
    public void SetCurrentBombTimer()
    {
        currentBombTimer = maxBombTimer;
    }
}
