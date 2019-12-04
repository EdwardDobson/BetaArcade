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
    SettingsMenu settings;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu = transform.GetChild(0).gameObject;
        WinScreen = GameObject.Find("GameManager").transform.GetChild(0).gameObject;
        settings = GameObject.Find("GameManager").transform.GetChild(1).GetComponent<SettingsMenu>();
    }
   public void ResetWinScreenObject()
    {
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
        EventSystem.current.SetSelectedGameObject(PauseMenu.transform.GetChild(1).GetChild(1).gameObject);
        settings.SetVolumeSliders();
    }
   public void Resume()
    {
        Time.timeScale = 1;
        paused = false;
        PauseMenu.SetActive(false);
        GameObject TutorialScreen = GameObject.Find("TutorialScreen");
        if(TutorialScreen != null)
        {

        
        if (TutorialScreen.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(TutorialScreen.transform.GetChild(0).GetChild(3).gameObject);
        }
        }
    }
    public void SetOptionsButton()
    {
        EventSystem.current.SetSelectedGameObject(PauseMenu.transform.GetChild(0).GetChild(1).gameObject);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        paused = false;
        Time.timeScale = 1;
    }
    public void SetBackButton()
    {
        EventSystem.current.SetSelectedGameObject(PauseMenu.transform.GetChild(1).GetChild(1).gameObject);
    }
}
