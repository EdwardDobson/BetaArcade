using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<int> levelPlaylist = new List<int>();
    [SerializeField]
    public List<Transform> Portraits = new List<Transform>();
    public List<GameObject> PlayerUIs = new List<GameObject>();
    public GameObject PlayerUI;
    int currentSceneID = 0;//Represents the element id
    int numberOfRounds = 2;//Set in lobby menu
    int playerTotal = 4;
    int playerCount = 0;
    GameObject winScreen;
    #region Scores
    //Manage your own rounds within your game scene then when somebody wins the round add to these values
    int playerOneScore = 0;
    int playerTwoScore = 0;
    int playerThreeScore = 0;
    int playerFourScore = 0;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        winScreen = transform.GetChild(0).gameObject;
        if(playerCount < 4)
        {
            for (int i = 0; i < playerTotal; ++i)
            {
                CreatePlayerUI();
                //Hides ui for the main menu
                /*
                foreach (Transform child in GameObject.Find("PlayerUI").transform.GetChild(1).transform)
                {
                    child.gameObject.SetActive(false);
                }
                */
            }
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        if(winScreen.activeSelf)
        {
            if(Input.GetButtonDown("Jump1"))
            {
                LoadLevel();
                winScreen.SetActive(false);

            }
        }
    }
    public void CreatePlayerUI()
    {
        GameObject playerUI = Instantiate(PlayerUI);
        if (playerCount == 0)
        {
            playerUI.GetComponent<Image>().color = Color.red;
            playerUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 0.3f);
            playerUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        if (playerCount == 1)
        {
            playerUI.GetComponent<Image>().color = Color.yellow;
            playerUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 0, 0.3f);
            playerUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        if (playerCount == 2)
        {
            playerUI.GetComponent<Image>().color = Color.green;
            playerUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 1, 0.3f);
            playerUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.green;
        }
        if (playerCount == 3)
        {
            playerUI.GetComponent<Image>().color = Color.blue;
            playerUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 1, 0.3f);
            playerUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.blue;
        }
        playerUI.transform.position = Portraits[playerCount].position;
        playerCount++;
        playerUI.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        playerUI.transform.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        playerUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Score: 0 ";
        playerUI.transform.SetParent(GameObject.Find("PlayerUI").transform.GetChild(1).transform);
        PlayerUIs.Add(playerUI);
    }
    public void CreatePlaylist(int _levelID)//Used to create the playlist of levels
    {
        levelPlaylist.Add(_levelID);
    }
    public void CreateRandomPlayList()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
     
        for(int i = 0; i< sceneCount; ++i)
        {
            int random = Random.Range(2, sceneCount);//dont include 1 or 0 that will be the main menu and splash screen
            levelPlaylist.Add(random);
        }
    }
    public void RandomPlaylistOrder()
    {
        for (int i = 0; i < levelPlaylist.Count; ++i)
        {
            int temp = levelPlaylist[i];
            int randomIndex = Random.Range(0, levelPlaylist.Count);
            levelPlaylist[i] = levelPlaylist[randomIndex];
            levelPlaylist[randomIndex] = temp;
        }
    }
    public void LoadLevel()
    {
        if(levelPlaylist.Count >= 1)
        {
            StartCoroutine(LoadAsync());
        }
    }
    IEnumerator LoadAsync()
    {
        AsyncOperation aysncLoad = SceneManager.LoadSceneAsync(levelPlaylist[currentSceneID]);
        currentSceneID++;//Moves the list along ready for the next level
        while (!aysncLoad.isDone)
        {
            yield return null;
        }
        //Used to reactive the player uis in the main menu
        /* 
        foreach (Transform child in GameObject.Find("PlayerUI").transform.GetChild(1).transform)
        {
            for(int i = 0; i< playerTotal; ++i)
            {

            child.gameObject.SetActive(true);
            }
        }
        */
    }
  
    #region ScoreSetters
    public void SetPlayerOneScore(int _set)
    {
        playerOneScore += _set;
    }
    public void SetPlayerTwoScore(int _set)
    {
        playerTwoScore += _set;
    }
    public void SetPlayerThreeScore(int _set)
    {
        playerThreeScore += _set;
    }
    public void SetPlayerFourScore(int _set)
    {
        playerFourScore += _set;
    }
    #endregion
    public void SetNumberOfRounds(int _set)//Set in lobby menu
    {
        numberOfRounds = _set;
    }
    public int GetNumberOfRounds()//Used at the start of your scene to set your own max round value or to just use 
    {
        return numberOfRounds;
    }

}
