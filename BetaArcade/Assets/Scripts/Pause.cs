using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Pause : MonoBehaviour
{
    bool paused = false;
    GameObject PauseMenu;
    GameObject WinScreen;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu = transform.GetChild(0).gameObject;
        WinScreen = GameObject.Find("GameManager").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       if(SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1 && !WinScreen.activeSelf)
        {
            if (Input.GetButtonDown("Start"))
            {
                if (paused)
                {
                    Resume();
                }
                else
                {
                    Paused();
                }
            }
        }
    }
    public void Paused()
    {
        Time.timeScale = 0;
        paused = true;
        PauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(PauseMenu.transform.GetChild(0).GetChild(1).gameObject);
    }
   public void Resume()
    {
        Time.timeScale = 1;
        paused = false;
        PauseMenu.SetActive(false);
    }
}
