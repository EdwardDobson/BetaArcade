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
    List<string> levelPlaylistNames = new List<string>();
    [SerializeField]
    public List<Transform> Portraits = new List<Transform>();
    public List<GameObject> PlayerPictures = new List<GameObject>();
    public GameObject PlayerPicture;
    public TextMeshProUGUI gameModeList;
    public TextMeshProUGUI playerTotalText;
    public TextMeshProUGUI notEnoughText;
    public TextMeshProUGUI playerTotalText2;
    public TextMeshProUGUI notEnoughText2;
    public TextMeshProUGUI roundCountText;
    public TextMeshProUGUI roundCountText2;
    GameObject PlayerUI;
    int currentSceneID = 0;//Represents the element id
    int numberOfRounds = 0;//Set in lobby menu
    int playerTotal = 0;
    int playerCount = 0;
    [SerializeField]
    int timer;
    GameObject winScreen;
    [SerializeField]
    int levelNameIndex = -1;
    #region Scores
    //Manage your own rounds within your game scene then when somebody wins the round add to these values
    [SerializeField]
    int playerOneScore = 0;
    [SerializeField]
    int playerTwoScore = 0;
    [SerializeField]
    int playerThreeScore = 0;
    [SerializeField]
    int playerFourScore = 0;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PlayerUI = GameObject.Find("PlayerUI");
        playerTotalText.text = "Player Total: " + playerTotal;
        winScreen = transform.GetChild(0).gameObject;
        gameModeList.text = "Game Modes \n";


    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex <= 1)
        {
            PlayerUI.transform.GetChild(1).gameObject.SetActive(false);

        }
        else PlayerUI.transform.GetChild(1).gameObject.SetActive(true);
        if (winScreen.activeSelf)
        {
            PlayerUI.transform.GetChild(1).gameObject.SetActive(false);
            if (Input.GetButtonDown("Jump1"))
            {
                LoadLevel();
                winScreen.SetActive(false);
            }
        }
    }
    public void RemoveLevel(string _name)
    {
        for (int i = 0; i < levelPlaylistNames.Count; ++i)
        {
            if (levelPlaylistNames[i] == _name)
            {
                levelPlaylistNames.RemoveAt(i);
                levelPlaylist.RemoveAt(i);
                levelNameIndex--;
                gameModeList.text = gameModeList.text.Replace("\n" + _name, "");

            }
        }
    }
    public void CreatePlayerUIButton()
    {
        if (playerTotal > 1 && levelPlaylist.Count > 0)
        {
            if (playerCount < 4)
            {
                for (int i = 0; i < playerTotal; ++i)
                {
                    CreatePlayerUI();
                }
            }
        }
    }
    public void ResetPlayerCount()
    {
        playerTotal = 0;
        playerTotalText.text = "Player Total: " + playerTotal;
        playerTotalText2.text = "Player Total: " + playerTotal;
    }
    public void IncreasePlayerCount()
    {
        if (playerTotal <= 3)
        {
            playerTotal++;
            playerTotalText.text = "Player Total: " + playerTotal;
            playerTotalText2.text = "Player Total: " + playerTotal;
        }

    }
    public void DecreasePlayerCount()
    {
        if (playerTotal > 1)
        {
            playerTotal--;
            playerTotalText.text = "Player Total: " + playerTotal;
            playerTotalText2.text = "Player Total: " + playerTotal;
        }

    }
    public void Quit()
    {
        Application.Quit();
    }
    public int GetPlayerCount()
    {
        return playerTotal;
    }
    public int GetTimer()
    {
        return timer;
    }
    public void SetTimer(int _timer)
    {
        timer = _timer;
    }
    public void DecreaseTimer()
    {
        timer--;
    }
    public void CreatePlayerUI()
    {
        GameObject playerUI = Instantiate(PlayerPicture);
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

        playerUI.transform.SetParent(GameObject.Find("PlayerUI").transform.GetChild(1).transform);
        PlayerPictures.Add(playerUI);
    }
    public void AddToPlayListName(string _levelName)
    {
        levelPlaylistNames.Add(_levelName);
        gameModeList.text += levelPlaylistNames[levelNameIndex] + "\n";
    }
    public void AddToPlaylist(int _levelID)//Used to create the playlist of levels
    {
        levelPlaylist.Add(_levelID);
        levelNameIndex++;
    }
    public void ResetPlaylist()
    {
        levelPlaylistNames.Clear();
        levelPlaylist.Clear();
        gameModeList.text = gameModeList.text.Replace(gameModeList.text, "Game Modes \n");
        levelNameIndex = -1;
    }

    public void CreateRandomPlayList()
    {
        if (playerTotal > 1 && numberOfRounds > 0)
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            for (int i = 0; i < sceneCount; ++i)
            {
                int random = Random.Range(2, sceneCount);//dont include 1 or 0 that will be the main menu and splash screen
                levelPlaylist.Add(random);
            }
            CreatePlayerUIButton();
            StartCoroutine(LoadAsync());
        }
    }
    public void RandomPlaylistOrder()
    {
        StartCoroutine(RandomPlaylist());
    }
    IEnumerator RandomPlaylist()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < levelPlaylist.Count; ++i)
        {
            int temp = levelPlaylist[i];
            int randomIndex = Random.Range(0, levelPlaylist.Count);
            levelPlaylist[i] = levelPlaylist[randomIndex];
            levelPlaylist[randomIndex] = temp;
            string temp2 = levelPlaylistNames[i];
            levelPlaylistNames[i] = levelPlaylistNames[randomIndex];
            levelPlaylistNames[randomIndex] = temp2;
            gameModeList.text = gameModeList.text.Replace(gameModeList.text, "Game Modes \n");
            ResetGameModeText();
        }
    }
    public void ResetGameModeText()
    {
        for (int i = 0; i < levelPlaylistNames.Count; ++i)
        {
            gameModeList.text += levelPlaylistNames[i] + "\n";
        }

    }
    public void LoadLevel()
    {
        if (levelPlaylist.Count >= 1 && playerTotal > 1 && numberOfRounds > 0)
        {
            StartCoroutine(LoadAsync());
        }
        if (playerTotal < 2)
        {
            notEnoughText.text = "Not enough players";
            StartCoroutine(ResetNotEnoughText());
        }
        if (levelPlaylist.Count <= 0)
        {
            notEnoughText.text = "No gamemodes in playlist";
            StartCoroutine(ResetNotEnoughText());
        }
        if (numberOfRounds <= 0)
        {
            notEnoughText.text = "Not enough rounds";
            StartCoroutine(ResetNotEnoughText());
        }
    }
    IEnumerator ResetNotEnoughText()
    {

        yield return new WaitForSeconds(1);
        notEnoughText.text = "";
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
    public void IncreaseNumberOfRounds()
    {
        numberOfRounds++;
        roundCountText.text = "Round Total: " + numberOfRounds;
        roundCountText2.text = "Round Total: " + numberOfRounds;
    }
    public void DecreaseNumberOfRounds()
    {
        numberOfRounds--;
        roundCountText.text = "Round Total: " + numberOfRounds;
        roundCountText2.text = "Round Total: " + numberOfRounds;
    }
    public int GetNumberOfRounds()//Used at the start of your scene to set your own max round value or to just use 
    {
        return numberOfRounds;
    }

}
