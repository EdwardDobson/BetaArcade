﻿using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        previousPos = transform;
        
        InvokeRepeating("MovePoint", 1, 5);
        pointsHolder = GameObject.Find("Points");
        for(int i =0; i< pointAmount; ++i)
        {
            points.Add(pointsHolder.GetComponent<Transform>().GetChild(i));
         
        }
        moveText = GameObject.Find("MoveText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(points.Count <=0)
        {
            for (int i = 0; i < pointAmount; ++i)
            {
                points.Add(pointsHolder.GetComponent<Transform>().GetChild(i));
            }
        }
    }
    void MovePoint()
    {
        if(!moved)
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
    IEnumerator HideText()
    {
        yield return new WaitForSeconds(1);
        moveText.text = "";
        moved = false;
    }
}