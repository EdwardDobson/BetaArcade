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
		roundText = GameObject.Find("RoundText").GetComponent<TextMeshProUGUI>();
		timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
		eliminationText = GameObject.Find("EliminationText").GetComponent<TextMeshProUGUI>();
		roundMax = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
		remainingPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerCount();
		roundText.text = "Round " + currentRound + " / " + gameManager.GetNumberOfRounds();
        eliminationText.alpha = 0;
	}
	void Restart()
	{
		roundMax = GameObject.Find("GameManager").GetComponent<GameManager>().GetNumberOfRounds();
		remainingPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerCount();
		isVictory = false;
	}
	private void FixedUpdate()
	{
		if(!isVictory)
		{
			roundTimer -= Time.deltaTime;
            int tempRoundTimer = Mathf.RoundToInt(roundTimer);
			timerText.text = "Time: " + tempRoundTimer;
		}
		
		if (roundTimer <= 0.0f && !isVictory)
		{
			isVictory = true;
			timerText.text = "Time Up!";
			Restart();
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
		StartCoroutine(FadeInText(eliminationText, 4.0f));
        eliminationText.text = "Player defeated!";
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
	IEnumerator FadeInText(TextMeshProUGUI text, float time)
	{
		while (text.color.a<1)
		{
			text.color -= new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / time));
		}
		StartCoroutine(FadeOutText(eliminationText, 4.0f));
		yield return null;

	}
	IEnumerator FadeOutText(TextMeshProUGUI text, float time)
	{
		while(text.color.a>1)
		{
			text.color -= new Color(text.color.r, text.color.g, text.color.b, text.color.a-(Time.deltaTime / time));
		}
		eliminationText.text = "";
		yield return null;
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
