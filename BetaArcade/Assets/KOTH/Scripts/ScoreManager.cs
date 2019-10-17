using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int score;
    [SerializeField]
    float timer;
    bool inPoint;
    bool dontAddScore;
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
        for (int i = 0; i < otherPlayers.Count; ++i)//Used to check if any other player is in the zone
        {
            if (otherPlayers[i].inPoint && inPoint)
            {
                timer = 0;
 
                point.gameObject.GetComponent<MeshRenderer>().material = materials[0];
            }
            if (inPoint  && !otherPlayers[i].inPoint)
            {
                timer += Time.deltaTime;
                if (timer >= 1)
                {
                    score += 1;
                    scoreText.text = "" + score;
                    scoreIncrease.Play();
                    timer = 0;
                }
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            inPoint = true;
            for(int i =0; i< otherPlayers.Count; ++i)//Used to check if any other player is in the zone
            {
                if(!otherPlayers[i].inPoint)
                {
                    if (gameObject.tag == "Player")
                    {
                        point.gameObject.GetComponent<MeshRenderer>().material = materials[1];
                    }
                    if (gameObject.tag == "Player2")
                    {
                        point.gameObject.GetComponent<MeshRenderer>().material = materials[2];
                    }
                    if (gameObject.tag == "Player3")
                    {
                        point.gameObject.GetComponent<MeshRenderer>().material = materials[3];
                    }
                    if (gameObject.tag == "Player4")
                    {
                        point.gameObject.GetComponent<MeshRenderer>().material = materials[4];
                    }
                }
            }
          
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            inPoint = false;
            dontAddScore = false;
            CancelInvoke("AddScore");
            point.gameObject.GetComponent<MeshRenderer>().material = materials[0];
            timer = 0;
        }
    }
    void AddScore()
    {
        score += 1;
        scoreText.text = "" + score;
        scoreIncrease.Play();
    }
}
