﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHotPotato : MonoBehaviour
{
    [SerializeField]
    bool hasBomb;
    [SerializeField]
    bool canTakeBomb = true;
    bool hasLeftEnemy;
    GameObject bombImage;
    GameManager gameManager;
    AudioSource stunned;
    void Start()
    {
        bombImage = transform.Find("SM_Bomb").gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stunned = transform.Find("StunnedAudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBomb)
        {
            bombImage.SetActive(true);
            canTakeBomb = false;
            gameManager.PlayerPictures[GetComponent<PlayerMove>().ID - 1].transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "has the bomb";
            GetComponent<PlayerMove>().SetSpeed(6);
        }
        if(!hasBomb)
        {
            bombImage.SetActive(false);
            gameManager.PlayerPictures[GetComponent<PlayerMove>().ID-1].transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "";
            GetComponent<PlayerMove>().SetSpeed(4);
        }
     
    }
    public void SetHasBomb(bool _bool)
    {
        hasBomb = _bool;
    }
    public void SetCanTakeBomb(bool _bool)
    {
        canTakeBomb = _bool;
    }
    public bool HasBomb()
    {
        return hasBomb;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!hasLeftEnemy)
        {
            if (other.gameObject.tag.Contains("Player"))
            {
                StartCoroutine(ResetCanTakeBomb());
                hasLeftEnemy = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            if (hasBomb && other.GetComponent<PlayerHotPotato>().canTakeBomb)
            {
                other.GetComponent<PlayerHotPotato>().SetHasBomb(true);
                hasBomb = false;
            }
            if (other.GetComponent<PlayerHotPotato>().hasBomb && canTakeBomb)
            {
                other.GetComponent<PlayerHotPotato>().SetHasBomb(false);
                StartCoroutine(ResetPlayerMovement());
                hasBomb = true;
            }
        }
    }
    IEnumerator ResetCanTakeBomb()
    {
        canTakeBomb = false;
        yield return new WaitForSeconds(1);
        canTakeBomb = true;
        hasLeftEnemy = false;
    }
    IEnumerator ResetPlayerMovement()
    {
        GetComponent<Rigidbody>().mass = 200;
        GetComponent<Rigidbody>().drag = 200;
        stunned.Play();
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody>().mass = 1;
        GetComponent<Rigidbody>().drag = 0;
    }
}
