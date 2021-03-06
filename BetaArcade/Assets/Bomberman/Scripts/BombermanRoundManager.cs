﻿using System.Collections;
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
	Slider timerSlider;
	[SerializeField]
	TextMeshProUGUI eliminationText;
	[SerializeField]
	TextMeshProUGUI countdownText;
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
	public bool isNeedTimer = false;
	public bool hasEnded = false;
	[SerializeField]
	Slider timeSlider;
	TextMeshProUGUI tutorialTimeText;
	[SerializeField]
	BombermanSpawn spawner;
	[SerializeField]
	AudioSource bgm;
	public bool gameStarted;
	public void SetHasStarted(bool started)
	{
		hasStarted = started;
		if (hasStarted)
		{
			if(!bgm.isPlaying)
			{
				bgm.Play(0);
			}
		}
		else if (!hasStarted)
		{
			bgm.Stop();
		}
	}
	public void SetIsTimerNeeded (bool timerNeeded)
	{
		isNeedTimer = timerNeeded;
		if(timerNeeded)
		{
			StartCoroutine("Countdown");
		}
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
		//timerSlider = GameObject.Find("RoundTimeSlider").GetComponent<Slider>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		roundText = GameObject.Find("RoundText").GetComponent<TextMeshProUGUI>();
		timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
		countdownText = GameObject.Find("CountdownText").GetComponent<TextMeshProUGUI>();
		eliminationText = GameObject.Find("EliminationText").GetComponent<TextMeshProUGUI>();
		roundMax = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
		remainingPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerCount();
		if (remainingPlayers < 4)
		{
			if (remainingPlayers == 3)
			{
				GameObject.Find("Player4 Text").GetComponent<TextMeshProUGUI>().text = "";
			}
			else if (remainingPlayers == 2)
			{
				GameObject.Find("Player4 Text").GetComponent<TextMeshProUGUI>().text = "";
				GameObject.Find("Player3 Text").GetComponent<TextMeshProUGUI>().text = "";
			}
		}
		spawner = GameObject.FindObjectOfType<BombermanSpawn>();
		roundText.text = "Round " + currentRound + " / " + gameManager.GetNumberOfRounds();
		eliminationText.text = "";
	}
	public void StartTime()
	{
		roundTimer = baseRoundTimer;
		timerText.text = "Time: " + roundTimer;
		CountdownTimer.Instance.Run();
		if (CountdownTimer.Instance.Timeleft <= 0)
		{
				gameStarted = true;
				isNeedTimer = false;
				isVictory = false;
				countdownText.text = "";
		
		
		}
	}
	//resets everything, applies points to victor(s)
	IEnumerator Restart()
	{
		gameStarted = false;
		yield return new WaitForSeconds(0.5f);

		int tmpID = 0;
		spawner.ResetPositions();
		List<GameObject> winners = spawner.RemainingPlayers();
		for (int i = 0; i < winners.Count; ++i)
		{
			winners[i].GetComponent<Bomberman>().ResetPowers(); //cycling
			tmpID = winners[i].GetComponent<PlayerMove>().ID;
			Debug.Log("ID " + tmpID);
			gameManager.SetPlayerScore(tmpID, 1);
			Debug.Log("Player " + tmpID + " Score= " + gameManager.GetPlayerScore(tmpID));
		}
		roundTimer = baseRoundTimer;
		remainingPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerCount();
		currentRound++;
		isVictory = false;
		//StartCoroutine(Countdown());
		isScoring = false;
		countdownText.text = "";
	
	}
	//used to start the game round
	
	IEnumerator Final()
	{
		yield return new WaitForSeconds(0.5f);
		hasEnded = true;
		int tmpID = 0;
		List<GameObject> winners = spawner.RemainingPlayers();
		for (int i = 0; i < winners.Count; ++i)
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
	private void Update()
	{
		
		if (hasStarted)
		{
			if (!gameStarted)
			{
				StartTime();
			}
			Debug.Log("asd");
			if (!isVictory && !isNeedTimer && gameStarted)
			{
			
				roundText.text = "Round: " + currentRound + " of " + gameManager.GetNumberOfRounds();
				if (roundTimer <= 0)
				{
					countdownText.text = "Time Up!";
					isScoring = true;
					isVictory = true;
					if(currentRound != roundMax)
					{
						StartCoroutine(Restart());
					}
					else if (currentRound == roundMax)
					{
						StartCoroutine(Final());
					}
				}
				roundTimer -= Time.deltaTime;
				int tempRoundTimer = Mathf.CeilToInt(roundTimer);
				timerText.text = "Time: " + tempRoundTimer;


				if (remainingPlayers <= 1 && currentRound != roundMax && !isScoring)
				{
					isScoring = true;
					isVictory = true;
					StartCoroutine(Restart());
				}
				else if (remainingPlayers <= 1 && currentRound >= roundMax && !isScoring)
				{
					isScoring = true;
					isVictory = true;
					StartCoroutine(Final());
				}
			}
		}
		if (!hasStarted && !isNeedTimer)
		{
			eliminationText.text = "";
			roundText.text = "";
			timerText.text = "";
			countdownText.text = "";
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

}
