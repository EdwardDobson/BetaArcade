using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class BombermanRoundManager : MonoBehaviour
{
	TextMeshProUGUI roundText;
	TextMeshProUGUI timerText;
	TextMeshProUGUI eliminationText;
	GameManager gameManager;
	[SerializeField]
	float baseRoundTimer = 60.0f;
	private float roundTimer = 0.0f;
	[SerializeField]
	int roundMax = 0;
	[SerializeField]
	int currentRound = 0;
	[SerializeField]
	int remainingPlayers = 1;
	[SerializeField]
	bool isVictory = false;

	// Start is called before the first frame update
	void Start()
    {
		roundTimer = baseRoundTimer;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		roundText = GameObject.Find("roundText").GetComponent<TextMeshProUGUI>();
		timerText = GameObject.Find("timerText").GetComponent<TextMeshProUGUI>();
		roundMax = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
		remainingPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerCount();
	}
	void Restart()
	{
		roundMax = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
		remainingPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerCount();
		isVictory = false;
	}
	private void FixedUpdate()
	{
		if(remainingPlayers>1 && !isVictory)
		{
			roundTimer -= Time.deltaTime;
			timerText.text = "Time: " + roundTimer;
			if (roundTimer <= 0.0f)
			{
				timerText.text = "Time Up!";
				Restart();
			}
		}
	}
	void Draw()
	{
		timerText.text = "Time: 0";
		isVictory = true;
		Invoke("Restart", 5.0f);
	}
	void Victory()
	{
		timerText.text = "Time: 0";
		isVictory = true;
		Invoke("Restart", 5.0f);
	}
	public void PlayerDown()
	{
		eliminationText.text = "Player defeated!";
		Invoke("CleanEliminationText", 3.5f);
		remainingPlayers--;
		if(remainingPlayers==1)
		{
			Victory();
		}
		if (remainingPlayers <= 1)
		{
			Draw();
		}
	}
	void CleanEliminationText()
	{
		eliminationText.text = "";
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
