using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    public int ID;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
            GetComponent<Renderer>().material.SetColor("_BaseColor", LightColour(ID));
            transform.GetChild(0).gameObject.SetActive(true);
       
        if (ID == 3 && gameManager.GetPlayerCount() > 2)
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", LightColour(ID));
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (ID == 4 && gameManager.GetPlayerCount() > 3)
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", LightColour(ID));
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (ID == 4 && gameManager.GetPlayerCount() < 4)
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        if (ID == 3 && gameManager.GetPlayerCount() < 3)
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private Color LightColour(int id)
    {
        switch (id)
        {
            case 1:
                return Color.red;
            case 2:
                return Color.yellow;
            case 3:
                return Color.green;
            case 4:
                return Color.blue;
            default:
             
                break;
        }
        return Color.clear;
    }
}
