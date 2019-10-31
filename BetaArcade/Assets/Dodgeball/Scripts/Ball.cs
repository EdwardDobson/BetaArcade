using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool IsActive = false;

    Dodgeball_PlayerSpawner DodgeballPlayerSpawner;
    //Win_Condition WC;

    public int PlayersDown = 0;

    private void Start()
    {
        DodgeballPlayerSpawner = GameObject.Find("Spawner").GetComponent<Dodgeball_PlayerSpawner>();
        //WC = GameObject.Find("Spawner").GetComponent<Win_Condition>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2" || col.gameObject.tag == "Player3" || col.gameObject.tag == "Player4")
        {
            if (IsActive) // if the player doesn't have anything
            {
                col.gameObject.SetActive(false);
                IsActive = false;
                PlayersDown++;
            }
            Debug.Log("Player Down");
        }
    }

    public void BallThrown()
    {
        IsActive = true;
    }

}
