using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotPotato : MonoBehaviour
{
    // Start is called before the first frame update
    #region Bomb
    public int maxBombTimer;
    [SerializeField]
    int currentBombTimer;
    #endregion
    #region PassBomb
    [SerializeField]
    public List<GameObject> players = new List<GameObject>();
    GameManager gameManager;
    #endregion
    [SerializeField]
    int increaseInactivePlayers;
    TextMeshProUGUI roundText;
    [SerializeField]
    int currentRound = 0;
    [SerializeField]
    int maxRound;
    bool endGameMode = false;
    void Start()
    {

        Invoke("LateStart", 0.1f);
        InvokeRepeating("BombTimer", 0, 1);
    }
    void LateStart()
    {
        roundText = GameObject.Find("RoundText").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentBombTimer = maxBombTimer;
        for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
        {
            int randomBombPick = Random.Range(0, gameManager.GetPlayerCount());
            if (!players[randomBombPick].GetComponent<PlayerHotPotato>().HasBomb())
            {
                players[randomBombPick].GetComponent<PlayerHotPotato>().SetHasBomb(true);

                break;
            }
        }
        currentRound++;
        maxRound = gameManager.GetNumberOfRounds();
        roundText.text = "Round: 1 of " + maxRound;
    }
    void BombTimer()
    {
        currentBombTimer--;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentRound > maxRound)
        {
            endGameMode = true;
            roundText.text = "";
            currentRound = maxRound;
        }
        if (!endGameMode)
        {
            if (increaseInactivePlayers >= gameManager.GetPlayerCount()-1)
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
                    gameManager.PlayerUIs[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "has the bomb";
                }
                if (!players[i].GetComponent<PlayerHotPotato>().HasBomb())
                {
                    gameManager.PlayerUIs[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "";
                }

            }
        }
    }
    void ResetRound()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);

            roundText.text = "Round: " + currentRound + " of " + maxRound;
            currentBombTimer = maxBombTimer;
            t.GetComponent<PlayerHotPotato>().SetCanTakeBomb(true);
            t.GetComponent<PlayerHotPotato>().SetHasBomb(false);
        }
        increaseInactivePlayers = 0;
    }
    IEnumerator ResetRoundCoroutine()
    {

        yield return new WaitForSeconds(1);
        ResetRound();
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
    }
    void ResetBomb()
    {
        if (currentBombTimer <= 0)
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
