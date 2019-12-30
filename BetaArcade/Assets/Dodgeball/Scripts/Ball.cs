using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool IsActive = false;

    public int PlayersDown = 0;

    private void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Player"))
        {
            if (this.CompareTag("B1") && col.CompareTag("Player1") || this.CompareTag("B2") && col.CompareTag("Player2") || this.CompareTag("B3") && col.CompareTag("Player3") || this.CompareTag("B4") && col.CompareTag("Player4"))
            {
                Debug.Log("Player hit themselves");
                return;
            }
            if (IsActive)
            {
                col.gameObject.SetActive(false);
                IsActive = false;
                PlayersDown++;
            }
            Debug.Log("Player Down");
        }

        if (col.gameObject.tag == "Ground")
        {
            IsActive = false;
        }

        this.tag = "Ball";
    }

    public void BallThrown()
    {
        IsActive = true;
    }

}
