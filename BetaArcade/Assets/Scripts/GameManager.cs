using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<int> levelPlaylist = new List<int>();
    int currentSceneID = 0;//Represents the element id
    int numberOfRounds = 0;//Set in lobby menu
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
