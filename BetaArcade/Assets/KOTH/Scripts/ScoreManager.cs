using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int score;
    PointMove point;

    // Start is called before the first frame update
    void Start()
    {
        point = GameObject.Find("Point").GetComponent<PointMove>();
        scoreText = GameObject.Find("Counter").GetComponent<TextMeshProUGUI>();
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            InvokeRepeating("AddScore", 0, 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            CancelInvoke("AddScore");
        }
    }
    void AddScore()
    {
        score += 1;
        scoreText.text = "" + score;
        Debug.Log("Add point");

    }
}
