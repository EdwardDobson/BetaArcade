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
    #region TutorialScreen
    public TextMeshProUGUI title;
    public TextMeshProUGUI howToPlayText;
    #endregion
    GameObject PlayerUI;
    [SerializeField]
    int currentSceneID = -1;//Represents the element id
    int numberOfRounds = 1;//Set in lobby menu
    int playerTotal = 2;
    int playerCount = 0;
    [SerializeField]
    int timer;
    [SerializeField]
    int oldtimer;
    GameObject winScreen;
    [SerializeField]
    int levelNameIndex = -1;
    TextMeshProUGUI nextLevelButtonText;
    [SerializeField]
    List<TextMeshProUGUI> endGameModeScoreTexts = new List<TextMeshProUGUI>();
    bool shouldLoad = true;
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
        nextLevelButtonText = transform.GetChild(0).GetChild(0).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>();
        roundCountText.text = "Round Total \nPer Game Mode: " + numberOfRounds;
        roundCountText2.text = "Round Total \nPer Game Mode: " + numberOfRounds;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerTotal >= 2)
        {
            endGameModeScoreTexts[0].text = "Player One \n Total Rounds \n" + playerOneScore;
            endGameModeScoreTexts[1].text = "Player Two \n Total Rounds \n" + playerTwoScore;
        }
        if (playerTotal >= 3)
        {
            endGameModeScoreTexts[2].text = "Player Three \nTotal Rounds \n" + playerThreeScore;
        }
        if (playerTotal >= 4)
        {
            endGameModeScoreTexts[3].text = "Player Four \n Total Rounds \n" + playerFourScore;
        }
        if (currentSceneID == levelPlaylist.Count - 1)
        {
            nextLevelButtonText.text = "Go to finish";
        }
        else if (currentSceneID < levelPlaylist.Count)
        {
            nextLevelButtonText.text = "Next Level";
        }
        if (winScreen.activeSelf)
        {

            if (Input.GetButtonDown("Jump1"))
            {
                LoadLevel();
                winScreen.SetActive(false);
            }
            PlayerUI.transform.GetChild(1).gameObject.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name != "MainMenu" && !winScreen.activeSelf)
        {
            PlayerUI.transform.GetChild(1).gameObject.SetActive(true);
        }
        if(SceneManager.GetActiveScene().name == "EndZone")
        {
            for(int i =0; i < playerTotal; ++i)
            {
                PlayerUI.transform.GetChild(1).GetChild(i).GetChild(6).GetComponent<TextMeshProUGUI>().text = "";
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
        if (playerTotal > 1 && levelPlaylist.Count > 0 && numberOfRounds > 0)
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
        playerTotal = 2;
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
        if (playerTotal > 2)
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
    public void DecreaseTimer(int _decrease)
    {
        timer -= _decrease;
    }
    public void IncreaseTimer(int _increase)
    {
        timer += _increase;
        oldtimer = timer;
    }
    public int GetOldTimer()
    {
        return oldtimer;
    }
    public void SetOldTimer()
    {
        oldtimer = timer;
    }
    public void CreatePlayerUI()
    {
        GameObject playerUI = Instantiate(PlayerPicture);

        if (playerCount == 0)
        {
            playerUI.GetComponent<Image>().color = Color.red;
            playerUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 0.3f);
            playerUI.transform.GetChild(7).GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 0.3f);
            playerUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
            playerUI.transform.GetChild(7).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        if (playerCount == 1)
        {
            playerUI.GetComponent<Image>().color = Color.yellow;
            playerUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 0, 0.3f);
            playerUI.transform.GetChild(7).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 1, 0.3f);
            playerUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.yellow;
            playerUI.transform.GetChild(7).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        if (playerCount == 2)
        {
            playerUI.GetComponent<Image>().color = Color.green;
            playerUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 0.3f);
            playerUI.transform.GetChild(7).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 0.3f);
            playerUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.green;
            playerUI.transform.GetChild(7).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.green;
        }
        if (playerCount == 3)
        {
            playerUI.GetComponent<Image>().color = Color.blue;
            playerUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 1, 0.3f);
            playerUI.transform.GetChild(7).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 1, 0.3f);
            playerUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.blue;
            playerUI.transform.GetChild(7).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.blue;
        }

        playerUI.transform.position = Portraits[playerCount].position;
        playerCount++;
        playerUI.name = "PlayerPicture" + playerCount;
        playerUI.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        playerUI.transform.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        playerUI.transform.GetChild(3).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        playerUI.transform.GetChild(4).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        playerUI.transform.GetChild(5).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        playerUI.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "";
        playerUI.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
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
        if (currentSceneID < levelPlaylist.Count)
        {
            if (shouldLoad == true)
            {

                shouldLoad = false;
                if (levelPlaylist.Count >= 1 && playerTotal > 1 && numberOfRounds > 0)
                {
                    StartCoroutine(LoadAsync());

                }
                if (playerTotal < 2)
                {
                    notEnoughText.text = "Not enough players";
                    StartCoroutine(ResetNotEnoughText());
                    shouldLoad = true;
                }
                if (levelPlaylist.Count <= 0)
                {
                    notEnoughText.text = "No gamemodes in playlist";
                    StartCoroutine(ResetNotEnoughText());
                    shouldLoad = true;
                }
                if (numberOfRounds <= 0)
                {
                    notEnoughText.text = "Not enough rounds";
                    StartCoroutine(ResetNotEnoughText());
                    shouldLoad = true;
                }
            }
        }
        if (currentSceneID >= levelPlaylist.Count)
        {
            StartCoroutine(LoadMainMenu());
        }

    }
    IEnumerator ResetNotEnoughText()
    {

        yield return new WaitForSeconds(1);
        notEnoughText.text = "";
    }
    public void ChangeTutorialScreen(string _gameName)
    {
        if (_gameName == "King Of The Hill")
        {
            title.text = _gameName;
            howToPlayText.text = "-Enter the zone to gain points. \n" + "-Shove other players out of the zone to stop them contesting.\n" + "-Highest points wins the round when the timer hits zero.\n" + "-Each round gains you a point to the overall score.";
        }
        if (_gameName == "Hot Potato")
        {
            title.text = _gameName;
            howToPlayText.text = "-Pass the bomb to other players. \n" + "-If you have the bomb when the timer runs out you will explode. \n" + "-Last player standing wins the round.\n" + "-Each round gains you a point to the overall score.";
        }
        if (_gameName == "Rising Lava")
        {
            title.text = _gameName;
            howToPlayText.text = "-Make your way up the platform to get away from the lava. \n" + "-Watch out for falling bombs. \n" + "-Last player standing wins the round.\n" + "-Each round gains you a point to the overall score.";
        }
        if (_gameName == "Paint The Floor")
        {
            title.text = _gameName;
            howToPlayText.text = "-Use your sludge gun to paint the floor in your sludge and gain points from it. \n" + "-Use your sludge to slow other players. \n" + "-Highest points wins the round when the timer hits zero.\n" + "-Each round gains you a point to the overall score.";
        }
        if (_gameName == "Paintball Tag")
        {
            title.text = _gameName;
            howToPlayText.text = "-Deathmatch style game.\n" + "-One hit from another players paint gun and you will have to wait to respawn.\n" + "-Highest eliminations at when the timer hits zero wins the round.";
        }
        if (_gameName == "Dodgeball")
        {
            title.text = _gameName;
            howToPlayText.text = "-Pick up the ball to throw it at other players.\n" + "-Get hit wait for respawn or the next round.\n" + "-Last man standing gains a point.";
        }
        if (_gameName == "Bomberman")
        {
            title.text = _gameName;
            howToPlayText.text = "-Place bombs to explode other players. \n" + "-Pick up power ups to enchance your bombs.\n" + "-Watch out though your own bombs can kill you.\n" + "-Last man standing gains a point.";
        }
        if (_gameName == "Tron")
        {
            title.text = _gameName;
            howToPlayText.text = "-Move around and try to hit other players with your trail. \n" + "-Watch out you can hit yourself with your own trail.\n" + "-Last man standing gains a point.";
        }
        if (_gameName == "Whack-A-Mole")
        {
            title.text = _gameName;
            howToPlayText.text = "-Move your circle around and press A to hit the moles. \n" + "-Highest mole eliminations wins the round and gains a point.\n";
        }
    }
    IEnumerator LoadMainMenu()
    {
            AsyncOperation aysncLoad = SceneManager.LoadSceneAsync("EndZone");
            currentSceneID = 0;
            while (!aysncLoad.isDone)
            {
                yield return null;
            }
            transform.GetChild(0).gameObject.SetActive(false);
            shouldLoad = true;
        PlayerUI = GameObject.Find("PlayerUI");
    }
    IEnumerator LoadAsync()
    {
        currentSceneID++;//Moves the list along ready for the next level
        if(currentSceneID < levelPlaylist.Count)
        {
            AsyncOperation aysncLoad = SceneManager.LoadSceneAsync(levelPlaylist[currentSceneID]);
            while (!aysncLoad.isDone)
            {
                yield return null;
            }
            transform.GetChild(0).gameObject.SetActive(false);
            shouldLoad = true;
            PlayerUI = GameObject.Find("PlayerUI");
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

    #region ScoreSetters&Getters
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
    public int GetPlayerOneScore()
    {
        return playerOneScore;
    }
    public int GetPlayerTwoScore()
    {
        return playerTwoScore;
    }
    public int GetPlayerThreeScore()
    {
        return playerThreeScore;
    }
    public int GetPlayerFourScore()
    {
        return playerFourScore;
    }
    #endregion
    public void SetNumberOfRounds(int _set)//Set in lobby menu
    {
        numberOfRounds = _set;
        roundCountText.text = "Round Total \nPer Game Mode: " + numberOfRounds;
        roundCountText2.text = "Round Total \nPer Game Mode: " + numberOfRounds;
    }
    public void IncreaseNumberOfRounds()
    {
        numberOfRounds++;
        roundCountText.text = "Round Total \nPer Game Mode: " + numberOfRounds;
        roundCountText2.text = "Round Total \nPer Game Mode: " + numberOfRounds;
    }
    public void DecreaseNumberOfRounds()
    {
        if (numberOfRounds > 1)
        {
            numberOfRounds--;
        }
        roundCountText.text = "Round Total \nPer Game Mode: " + numberOfRounds;
        roundCountText2.text = "Round Total \nPer Game Mode: " + numberOfRounds;
    }
    public int GetNumberOfRounds()//Used at the start of your scene to set your own max round value or to just use 
    {
        return numberOfRounds;
    }

}
