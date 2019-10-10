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
    int pointID;
    // Start is called before the first frame update
    void Start()
    {
        previousPos = transform;
        InvokeRepeating("MovePoint", 1, 5);
        moveText = GameObject.Find("MoveText").GetComponent<TextMeshProUGUI>();
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
            moveText.text = "Point has moved!";
            StartCoroutine(HideText());
        }
    
    }
    IEnumerator HideText()
    {
        yield return new WaitForSeconds(1);
        moveText.text = "";
    }
}
