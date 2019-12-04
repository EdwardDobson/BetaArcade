using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointMove : MonoBehaviour
{
    [SerializeField]
    List<Transform> points = new List<Transform>();
    Transform previousPos;
    TextMeshProUGUI moveText;
    GameObject pointsHolder;
    int pointID;
    [SerializeField]
    int pointAmount = 0;
    int tempPointAmount;
    bool clearingPoints;
    bool moved;
    [SerializeField]
    float timer;
    [SerializeField]
    float maxtimer;
    ScoreManager scoreManager;
    public float scoreAmountGive;
    public float maxscoreAmountGive;
    float percentage;
    // Start is called before the first frame update
    void Start()
    {

        scoreManager = GetComponent<ScoreManager>();
     
        previousPos = transform;
        //InvokeRepeating("MovePoint", 5, movePointSpeed);
        pointsHolder = GameObject.Find("Points");

        moveText = GameObject.Find("MoveText").GetComponent<TextMeshProUGUI>();
        timer = maxtimer;
    }
    public void SetPointGive()
    {
        percentage =  scoreManager.maxScore / 4;
        scoreAmountGive = percentage;
        maxscoreAmountGive = scoreAmountGive;
    }
    // Update is called once per frame
    void Update()
    {
        if (scoreManager.GetStartGame() == true)
        {
            if(scoreAmountGive <=0)
            {
                scoreAmountGive = maxscoreAmountGive;
            }
            if (points.Count <= 0)
            {
                for (int i = 0; i < pointAmount; ++i)
                {
                    points.Add(pointsHolder.GetComponent<Transform>().GetChild(i));

                }

            }
            if (scoreManager.GetStartGame())
            {
                MovePoint();
            }

        }

    }
    public void ResetPoints()
    {
        if (clearingPoints)
        {
            points.Clear();
            clearingPoints = false;

        }
        if (!clearingPoints)
        {
            for (int i = 0; i < pointAmount; ++i)
            {
                points.Add(pointsHolder.GetComponent<Transform>().GetChild(i));
                transform.position = pointsHolder.GetComponent<Transform>().GetChild(0).position;
                tempPointAmount++;
            }
            if (tempPointAmount >= 4)
            {
                clearingPoints = false;
                tempPointAmount = 0;
            }
        }

    }
    public void MovePoint()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {

            timer = maxtimer;
            if (!moved)
            {
                for (int i = 0; i < 1; ++i)
                {
                    previousPos.position = transform.position;
                    pointID = Random.Range(0, points.Count);
                    transform.position = points[pointID].transform.position;
                    points.RemoveAt(pointID);
                    moved = true;
                    moveText.text = "Point has moved!";
                    StartCoroutine(HideText());
                }
            }
        }
    }
    public void ForceMove()
    {
        for (int i = 0; i < 1; ++i)
        {
            previousPos.position = transform.position;
            pointID = Random.Range(0, points.Count);
            transform.position = points[pointID].transform.position;
            points.RemoveAt(pointID);
            moved = true;
            moveText.text = "Point has moved!";
            StartCoroutine(HideText());
        }
    }
    public void ResetTimer()
    {
        timer = maxtimer;
    }
    IEnumerator HideText()
    {
        yield return new WaitForSeconds(1);
        moveText.text = "";
        moved = false;
    }
    public void SetResetPoints(bool _state)
    {
        clearingPoints = _state;
    }
}
