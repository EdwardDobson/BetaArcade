using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool IsActive = false;

    Dodgeball_PlayerSpawner DodgeballPlayerSpawner;

    private void Start()
    {
        DodgeballPlayerSpawner = GameObject.Find("Spawner").GetComponent<Dodgeball_PlayerSpawner>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2" || col.gameObject.tag == "Player3" || col.gameObject.tag == "Player4")
        {
            if (IsActive) // if the player doesn't have anything
            {
                col.gameObject.SetActive(false);
                IsActive = false;
            }
            Debug.Log("Player Down");
            DodgeballPlayerSpawner.DecreasePlayerCount();
        }
    }
}
