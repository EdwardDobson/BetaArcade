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


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2" || col.gameObject.tag == "Player3" || col.gameObject.tag == "Player4")
        {
            if (IsActive) // if the player doesn't have anything
                col.gameObject.SetActive(false);
            Debug.Log("Player Down");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
