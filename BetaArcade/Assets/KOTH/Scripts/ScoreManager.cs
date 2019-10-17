using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI winText;
    TextMeshProUGUI scoreText;
    public int maxScore;
    bool resetPoints;
    int scoreIncreaseValue = 1;
    int resetPointsCounter;
    [SerializeField]
    float timer;
    PointMove point;
    [SerializeField]
    public Material pointMat;
    AudioSource scoreIncrease;
    [SerializeField]
    List<PointCollide> otherPlayers = new List<PointCollide>();
    // Start is called before the first frame update
    void Start()
    {
        point = GameObject.Find("Point").GetComponent<PointMove>();
        scoreText = GameObject.Find("Counter").GetComponent<TextMeshProUGUI>();
        scoreIncrease = GameObject.Find("Points").GetComponent<AudioSource>();
        scoreText.text = "0";
        winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < otherPlayers.Count; ++i)//Used to check if any other player is in the zone
        {
            for (int a = 0; a < otherPlayers.Count; ++a)//Used to check if any other player is in the zone
            {
                if (otherPlayers[i] != otherPlayers[a])
                {
                    if (otherPlayers[i].GetinPoint() && otherPlayers[a].GetinPoint() || !otherPlayers[i].GetinPoint() && !otherPlayers[a].GetinPoint())
                    {
                        timer = 0;
                        point.gameObject.GetComponent<MeshRenderer>().material = pointMat;
                        point.GetComponentInChildren<Light>().color = pointMat.color;
                        otherPlayers[i].SetinPoint(false);
                        otherPlayers[a].SetinPoint(false);
                    }
                    if (otherPlayers[i].GetinPoint() && !resetPoints)
                    {
                        point.gameObject.GetComponent<MeshRenderer>().material = otherPlayers[i].pointMat;
                        point.GetComponentInChildren<Light>().color = otherPlayers[i].pointMat.color;
                        timer += Time.deltaTime;
                        if (timer >= 1)
                        {
                            otherPlayers[i].SetScore(scoreIncreaseValue);
                            scoreText.text = "" + otherPlayers[i].GetScore();
                            scoreIncrease.Play();
                            timer = 0;
                        }
                    }
                    if (otherPlayers[i].GetScore() >= maxScore)
                    {
                        winText.text = otherPlayers[i].tag + " wins";
                        resetPoints = true;
                     
                    }
                }
            }
           
        }
        if (resetPoints == true)
        {
            for (int i = 0; i < otherPlayers.Count; i++)
            {
                otherPlayers[i].ResetScore(0);
                resetPointsCounter++;
                scoreText.text = "" + otherPlayers[i].GetScore();
            }
            if(resetPointsCounter >= otherPlayers.Count)
            {
                resetPoints = false;
                point.MovePoint();
            }
        }
    }
    public bool GetresetPoints()
    {
        return resetPoints;
    }
    public void SetresetPoints(bool _reset)
    {
         resetPoints = _reset;
    }
}
