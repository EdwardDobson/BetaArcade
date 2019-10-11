using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    List<PlayerHotPotato> players = new List<PlayerHotPotato>();
    bool switchPlayer;
    #endregion
    int increaseInactivePlayers;
    void Start()
    {
        currentBombTimer = maxBombTimer;
        for(int i =0; i< players.Count; ++i)
        {
            int randomBombPick = Random.Range(0, players.Count);
            if (!players[randomBombPick].HasBomb())
            {
                players[randomBombPick].SetHasBomb(true);
                break;
            }
        }
        InvokeRepeating("BombTimer", 0, 1);
    }
    void BombTimer()
    {
        currentBombTimer--;
    }
    // Update is called once per frame
    void Update()
    {
        ResetRound();
        ResetBomb();
        SwitchPlayer();


    }
    void ResetRound()
    {
        if (increaseInactivePlayers >= players.Count)
        {
            for (int i = 0; i < players.Count; ++i)
            {
                players[i].gameObject.SetActive(true);
              
                increaseInactivePlayers = 0;
            }
        }
    }
    void ResetBomb()
    {
        if (currentBombTimer <= 0)
        {
            for (int i = 0; i < players.Count; ++i)
            {
                if (players[i].HasBomb())
                {
                    players[i].SetHasBomb(false);
                    players[i].gameObject.SetActive(false);
                    currentBombTimer = maxBombTimer;
                    switchPlayer = true;
                    increaseInactivePlayers++;
                    break;
                }
            }
        }
    }
    void SwitchPlayer()
    {
        if (switchPlayer)
        {
            for (int i = 0; i < players.Count; ++i)
            {
                int randomBombPick = Random.Range(0, players.Count);
                if (players[randomBombPick].gameObject.activeSelf)
                {
                  
                    if (!players[randomBombPick].HasBomb())
                    {
                        players[randomBombPick].SetHasBomb(true);
                        switchPlayer = false;
                        break;
                    }
                }
            }
        }
    }
}
