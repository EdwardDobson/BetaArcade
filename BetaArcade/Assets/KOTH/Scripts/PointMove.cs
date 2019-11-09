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
    bool moved;
    [SerializeField]
    float timer;
    [SerializeField]
    float maxtimer;
    GameManager gameManager;
    ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreManager = GetComponent<ScoreManager>();
        previousPos = transform;
        //InvokeRepeating("MovePoint", 5, movePointSpeed);
        pointsHolder = GameObject.Find("Points");
        for(int i =0; i< pointAmount; ++i)
        {
            points.Add(pointsHolder.GetComponent<Transform>().GetChild(i));
            transform.position = pointsHolder.GetComponent<Transform>().GetChild(0).position;
        }
        moveText = GameObject.Find("MoveText").GetComponent<TextMeshProUGUI>();
        timer = maxtimer;
    }

    // Update is called once per frame
    void Update()
    {
       if(scoreManager.GetStartGame() == true)
        {
            if (points.Count <= 0)
            {
                for (int i = 0; i < pointAmount; ++i)
                {
                    points.Add(pointsHolder.GetComponent<Transform>().GetChild(i));
                }

            }
            MovePoint();
        }
      
    }
   public void MovePoint()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
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
    IEnumerator HideText()
    {
        yield return new WaitForSeconds(1);
        moveText.text = "";
        moved = false;
    }
}
