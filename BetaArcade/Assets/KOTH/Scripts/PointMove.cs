using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    [SerializeField]
    List<Transform> points = new List<Transform>();
    Transform previousPos;
    int pointID;
    // Start is called before the first frame update
    void Start()
    {
        previousPos = transform;
        InvokeRepeating("MovePoint", 1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MovePoint()
    {
        for(int i =0; i<points.Count; ++i)
        {
            previousPos.position = transform.position;
            pointID = Random.Range(0, points.Count);
            transform.position = points[pointID].transform.position;
            if (previousPos.position == transform.position)
            {
                pointID = Random.Range(0, points.Count);
                transform.position = points[pointID].transform.position;
            }
        }
    
    }
}
