using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if(gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
            GetComponent<Renderer>().material.SetColor("_Colour", LevelManagerTools.PlayerIDToColor(ID));
            transform.GetChild(0).gameObject.SetActive(true);
      
        if (ID == 3 && gameManager.GetPlayerCount() > 2)
        {
            GetComponent<Renderer>().material.SetColor("_Colour", LevelManagerTools.PlayerIDToColor(ID));
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (ID == 4 && gameManager.GetPlayerCount() > 3)
        {
            GetComponent<Renderer>().material.SetColor("_Colour", LevelManagerTools.PlayerIDToColor(ID));
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (ID == 4 && gameManager.GetPlayerCount() < 4)
        {
            GetComponent<Renderer>().material.SetColor("_Colour", Color.white);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        if (ID == 3 && gameManager.GetPlayerCount() < 3)
        {
            GetComponent<Renderer>().material.SetColor("_Colour", Color.white);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
