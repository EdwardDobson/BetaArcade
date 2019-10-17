using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollide : MonoBehaviour
{
    bool inPoint;
    [SerializeField]
    int score;
    public Material pointMat;
 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            inPoint = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            inPoint = false;
        }
    }
    public int GetScore()
    {
        return score;
    }
    public void SetScore(int _increase)
    {
        score += _increase;
    }
    public void ResetScore(int _increase)
    {
        score = _increase;
    }
    public bool GetinPoint()
    {
        return inPoint;
    }
    public void SetinPoint(bool _inPoint)
    {
        inPoint = _inPoint;
    }
}
