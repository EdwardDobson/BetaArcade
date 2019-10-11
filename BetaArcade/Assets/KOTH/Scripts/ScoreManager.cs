using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int score;
    bool inPoint;
    PointMove point;
    [SerializeField]
    List<Material> materials = new List<Material>();
    AudioSource scoreIncrease;
    [SerializeField]
    List<ScoreManager> otherPlayers = new List<ScoreManager>();
    // Start is called before the first frame update
    void Start()
    {
        point = GameObject.Find("Point").GetComponent<PointMove>();
        scoreText = GameObject.Find("Counter").GetComponent<TextMeshProUGUI>();
        scoreIncrease = GameObject.Find("Points").GetComponent<AudioSource>();
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
            inPoint = true;
            for(int i =0; i< otherPlayers.Count; ++i)//Used to check if any other player is in the zone
            {
                if(otherPlayers[i].inPoint != true)
                {
                    InvokeRepeating("AddScore", 0, 1);
                    point.gameObject.GetComponent<MeshRenderer>().material = materials[1];
                }
            }
          
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            inPoint = false;
            CancelInvoke("AddScore");
            point.gameObject.GetComponent<MeshRenderer>().material = materials[0];
        }
    }
    void AddScore()
    {
        score += 1;
        scoreText.text = "" + score;
        scoreIncrease.Play();
    }
}
