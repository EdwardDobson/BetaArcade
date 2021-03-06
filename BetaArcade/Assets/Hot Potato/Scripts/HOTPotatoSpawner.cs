﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOTPotatoSpawner : MonoBehaviour
{
    public GameObject Player;
    [SerializeField]
    public List<Transform> SpawnPoints = new List<Transform>();
    private int playerCount = 0;
    HotPotato hotPotato;
    GameManager gameManager;
    // Start is called before the first frame update

    //void Awake()
    //{
    //    Invoke("LateStart", 0.1f);
    //}
    private void Awake()
    { 
     gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hotPotato = GetComponent<HotPotato>();
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
        player.tag = "Player" + playerCount;
        LevelManagerTools.SetPlayerColor(player, playerCount);
        //player.GetComponent<Renderer>().material.SetColor("_Color", PlayerIDToColor(playerCount));
        player.GetComponent<PlayerMove>().ID = playerCount;
        hotPotato.players.Add(player);
        player.transform.SetParent(transform);
    }
}
