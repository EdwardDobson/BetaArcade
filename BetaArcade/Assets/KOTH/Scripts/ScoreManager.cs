using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI winText;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI inPointText;
    public int maxScore;
    bool resetPoints = false;
    bool canGainPoints = true;
    int scoreIncreaseValue = 1;
    int resetPointsCounter;
    [SerializeField]
    float timer;
    PointMove point;
    [SerializeField]
    public Material pointMat;
    AudioSource scoreIncrease;
    int inPointCount;
    KOTHPlayerSpawner KOTHPlayerSpawner;
    [SerializeField]
    public List<GameObject> otherPlayers = new List<GameObject>();
    [SerializeField]
    int playerOneInGameScore;
    [SerializeField]
    int playerTwoInGameScore;
    [SerializeField]
    int playerThreeInGameScore;
    [SerializeField]
    int playerFourInGameScore;
    //temp values
    [SerializeField]
    int currentRound;
    [SerializeField]
    int maxRound = 0; 
    // Start is called before the first frame update
    void Start()
    {
        point = GetComponent<PointMove>();
        scoreText = GameObject.Find("Counter").GetComponent<TextMeshProUGUI>();
        scoreIncrease = GameObject.Find("Points").GetComponent<AudioSource>();
        scoreText.text = "0";
        winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        inPointText = GameObject.Find("inPointText").GetComponent<TextMeshProUGUI>();
        KOTHPlayerSpawner = GetComponent<KOTHPlayerSpawner>();
        //maxRound = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
    }

    // Update is called once per frame
    void Update()
    {
        if(canGainPoints && currentRound < maxRound)
        {

        AddScore();
        }
        if(currentRound >= maxRound)
        {
            inPointText.text = "";
          
        }


    }
    void AddScore()
    {
        for (int i = 0; i < otherPlayers.Count; ++i)//Used to check if any other player is in the zone
        {
            if (inPointCount <= 1 && otherPlayers[i].GetComponent<PointCollide>().GetScore() != maxScore)
            {
                if (otherPlayers[i].GetComponent<PointCollide>().GetinPoint() && !resetPoints)
                {
                    inPointText.text = otherPlayers[i].tag + " is on the point";
                    point.gameObject.GetComponent<MeshRenderer>().material = otherPlayers[i].GetComponent<PointCollide>().pointMat;
                    timer += Time.deltaTime;
                    if (timer >= 1)
                    {
                        otherPlayers[i].GetComponent<PointCollide>().SetScore(scoreIncreaseValue);
                        scoreText.text = "" + otherPlayers[i].GetComponent<PointCollide>().GetScore();
                        scoreIncrease.Play();
                        timer = 0;
                    }
                }
            }
            else if (inPointCount >= 2)
            {
                point.gameObject.GetComponent<MeshRenderer>().material = pointMat;
                inPointText.text = "Point is being contested";
            }
            if (otherPlayers[i].GetComponent<PointCollide>().GetScore() >= maxScore)
            {
                if(otherPlayers[i].tag == "Player1")
                {
                    playerOneInGameScore++;
                }
                if (otherPlayers[i].tag == "Player2")
                {
                    playerTwoInGameScore++;
                }
                if (otherPlayers[i].tag == "Player3")
                {
                    playerThreeInGameScore++;
                }
                if (otherPlayers[i].tag == "Player4")
                {
                    playerFourInGameScore++;
                }
                winText.text = otherPlayers[i].tag + " wins the round";
             
                currentRound++;
                resetPoints = true;
            }
        }
        if (resetPoints == true)
        {
            for (int i = 0; i < otherPlayers.Count; i++)
            {
                otherPlayers[i].GetComponent<PointCollide>().ResetScore(0);
                resetPointsCounter++;
                scoreText.text = "" + otherPlayers[i].GetComponent<PointCollide>().GetScore();
                canGainPoints = false;
                otherPlayers[i].transform.position = KOTHPlayerSpawner.SpawnPoints[i].position;
                inPointText.text = "";
            }
            if (resetPointsCounter >= otherPlayers.Count)
            {
                resetPoints = false;
                point.MovePoint();
                StartCoroutine(CanGainPoints());
            }
        }
    }
    IEnumerator CanGainPoints()
    {
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            otherPlayers[i].GetComponent<Rigidbody>().mass = 200;
        }
        yield return new WaitForSeconds(2);
        canGainPoints = true;
        winText.text = "";
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            otherPlayers[i].GetComponent<Rigidbody>().mass = 1;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
        {
            point.gameObject.GetComponent<MeshRenderer>().material = pointMat;
        }
        if (other.gameObject.tag == "Player1")
        {
            inPointCount--;
        }
        if (other.gameObject.tag == "Player2")
        {
            inPointCount--;
        }
        if (other.gameObject.tag == "Player3")
        {
            inPointCount--;
        }
        if (other.gameObject.tag == "Player4")
        {
            inPointCount++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.tag == "Player1")
        {
            inPointCount++;
        }
        if (other.gameObject.tag == "Player2")
        {
            inPointCount++;
        }
        if (other.gameObject.tag == "Player3")
        {
            inPointCount++;
        }
        if (other.gameObject.tag == "Player4")
        {
            inPointCount++;
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
