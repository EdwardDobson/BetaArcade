﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class SelectTutorialStart : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject startButton;
    GameManager gameManager;
    [SerializeField]
    PlayerMove[] playersArray;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(startButton);
        Invoke("LateStart", 0.01f);
    }
    void LateStart()
    {
        playersArray =  FindObjectsOfType<PlayerMove>();
        for (int i = 0; i < playersArray.Length; ++i)
        {
            playersArray[i].GetComponent<PlayerMove>().canMove = false;
        }
     
    }
    public void AllowPlayerMove()
    {
        for (int i = 0; i < playersArray.Length; ++i)
        {
            playersArray[i].GetComponent<PlayerMove>().canMove = true;
        }
    }
    public void Update()
    {
        if(SceneManager.GetActiveScene().name == "EndZone")
        {
            playersArray = FindObjectsOfType<PlayerMove>();
            for (int i = 0; i < playersArray.Length; ++i)
            {
                playersArray[i].GetComponent<PlayerMove>().canMove = true;
            }
        }
    }
}
