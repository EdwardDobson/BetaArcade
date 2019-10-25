using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool IsActive = false;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    Dodgeball_PlayerSpawner DodgballPlayerSpawner;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == Player1 || col.gameObject == Player2 || col.gameObject == Player3 || col.gameObject == Player4)
        {
            if (IsActive) // if the player doesn't have anything
                col.gameObject.SetActive(false);
            Debug.Log("Player Down");
            DodgballPlayerSpawner.DecreasePlayerCount();
        }
    }
}
