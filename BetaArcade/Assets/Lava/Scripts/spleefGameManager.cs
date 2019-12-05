using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spleefGameManager : MonoBehaviour
{

    public GameObject[] players;
    public bool finished;
    public GameObject winner;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
