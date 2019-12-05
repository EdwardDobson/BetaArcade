using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class BombermanRoundManager : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI roundText;
	[SerializeField]
	TextMeshProUGUI timerText;
	[SerializeField]
	TextMeshProUGUI eliminationText;
	GameManager gameManager;
	[SerializeField]
	float baseRoundTimer = 60.0f;
	private float roundTimer = 0.0f;
	[SerializeField]
	int roundMax = 0;
	[SerializeField]
	int currentRound = 1;
	[SerializeField]
	int remainingPlayers = 1;
	[SerializeField]
	bool isVictory = false;
	[SerializeField]
	bool isScoring = false;
	public bool hasStarted = false;
	public bool hasInitialised = false;
	[SerializeField]
	Slider timeSlider;
	TextMeshProUGUI tutorialTimeText;
	[SerializeField]
	BombermanSpawn spawner;

	public void SetHasStarted(bool started)
	{
		hasStarted = started;
	}
	public void SetRoundTimer()
	{
		baseRoundTimer = ((int)timeSlider.value);
		tutorialTimeText.text = "Round Time : " + baseRoundTimer;
	}
	// Start is called before the first frame update
	void Start()
    {
		currentRound = 1;
		roundTimer = baseRoundTimer;
		GameObject.Find("RoundTimeSlider").GetComponent<Slider>();
		GameObject.Find("RoundTimerText").GetComponent<TextMeshProUGUI>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		roundText = GameObject.Find("RoundText").GetComponent<TextMeshProUGUI>();
		timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
		eliminationText = GameObject.Find("EliminationText").GetComponent<TextMeshProUGUI>();
		roundMax = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
		remainingPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerCount();
		if(remainingPlayers < 4)
		{
			if(remainingPlayers == 3)
			{
				GameObject.Find("Player4 Text").GetComponent<TextMeshProUGUI>().text = "";
			}
			else if(remainingPlayers == 2)
			{
				GameObject.Find("Player4 Text").GetComponent<TextMeshProUGUI>().text = "";
				GameObject.Find("Player3 Text").GetComponent<TextMeshProUGUI>().text = "";
			}
		}
		spawner = GameObject.FindObjectOfType<BombermanSpawn>();
		roundText.text = "Round " + currentRound + " / " + gameManager.GetNumberOfRounds();
		eliminationText.text = "";
	}
	//resets everything, applies points to victor(s)
	IEnumerator Restart()
	{
		yield return new WaitForSeconds(0.5f);
		int tmpID = 0;
		spawner.ResetPositions();
		List<GameObject> winners = spawner.RemainingPlayers();
		for(int i = 0; i < winners.Count; ++i)
		{
			tmpID = winners[i].GetComponent<PlayerMove>().ID;
			Debug.Log("ID " + tmpID);
			gameManager.SetPlayerScore(tmpID, 1);
			Debug.Log("Player " + tmpID + " Score= " + gameManager.GetPlayerScore(tmpID));
		}
		roundTimer = baseRoundTimer;
		remainingPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerCount();
		isVictory = false;
	}
	IEnumerator Final()
	{
		yield return new WaitForSeconds(0.5f);
		int tmpID = 0;
		List<GameObject> winners = spawner.RemainingPlayers();
		for(int i = 0; i < winners.Count; ++i)
		{
			tmpID = winners[i].GetComponent<PlayerMove>().ID;
			Debug.Log("ID " + tmpID);
			gameManager.SetPlayerScore(tmpID, 1);
		}
		roundText.text = "";
		timerText.text = "";
		eliminationText.text = "";
		gameManager.transform.GetChild(0).gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Next Level"));
	}
	private void FixedUpdate()
	{
		if(hasStarted)
		{
			if (!isVictory)
			{
				roundTimer -= Time.deltaTime;
				int tempRoundTimer = Mathf.RoundToInt(roundTimer);
				timerText.text = "Time: " + tempRoundTimer;
			}

			if (remainingPlayers <= 1 && currentRound != roundMax && !isScoring)
			{
				isScoring = true;
				isVictory = true;
				StartCoroutine(Restart());
			}
			else if (remainingPlayers <= 1 && currentRound == roundMax && !isScoring)
			{
				isScoring = true;
				isVictory = true;
				StartCoroutine(Final());
			}
		}
		if(!hasStarted)
		{
			eliminationText.text = "";
			roundText.text = "";
			timerText.text = "";
		}
	}
	public void PlayerDown(int playerID)
	{
		StartCoroutine(EliminatedPlayer(eliminationText, 2.5f, playerID));
        remainingPlayers--;
	}

	IEnumerator EliminatedPlayer(TextMeshProUGUI text, float delay, int id)
	{
		eliminationText.text = "Player " + id + " defeated!";
		yield return new WaitForSeconds(delay);
		eliminationText.text = "";
		yield return null;
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
