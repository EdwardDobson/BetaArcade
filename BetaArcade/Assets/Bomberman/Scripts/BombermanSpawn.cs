using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BombermanSpawn : MonoBehaviour
{
	public GameObject Player;
	public List<Transform> SpawnPoints = new List<Transform>();
	public List<GameObject> players = new List<GameObject>();
	Transform playerHolder;
	private int playerCount = 0;
	GameManager gameManager;
	BombermanRoundManager roundManager;
	// Start is called before the first frame update
	void Start()
	{
		roundManager = GameObject.FindObjectOfType<BombermanRoundManager>();
		playerHolder = GameObject.Find("PlayerHolder").transform;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		LateStart();
	}
	private void LateStart()
	{
		if (playerCount < gameManager.GetPlayerCount())
		{
			for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
			{
				CreatePlayer();
			}
		}
	}
	// Update is called once per frame
	void Update()
	{
		if(roundManager.hasStarted && !roundManager.hasInitialised)
		{
			roundManager.hasInitialised = true;
			
			HazardSpawner spawner = GameObject.FindObjectOfType<HazardSpawner>();
			spawner.SetActive(true);
		}
	}
	public void CreatePlayer()
	{
		GameObject player = Instantiate(Player);

		player.transform.position = SpawnPoints[playerCount].position;
		playerCount++;
		player.tag = "Player" + playerCount;
		player.GetComponent<Renderer>().material.SetColor("_Color", LevelManagerTools.PlayerIDToColor(playerCount));
		player.GetComponent<PlayerMove>().ID = playerCount;
		player.transform.SetParent(playerHolder);
		players.Add(player);
	}
	public void ResetPositions()
	{
		for (int i = 0; i < gameManager.GetPlayerCount(); ++i)
		{
			players[i].transform.position = SpawnPoints[i].position;
			players[i].GetComponent<Bomberman>().ResetPowers();
			players[i].SetActive(true);
		}
	}
	public List<GameObject> RemainingPlayers()
	{
		List<GameObject> tmp = new List<GameObject>();
		for(int i = 0; i < gameManager.GetPlayerCount(); ++i)
		{
			if(players[i].GetComponent<Bomberman>().GetIsDead() == false)
			{
				tmp.Add(players[i]);
			}
		}
		return tmp;
	}
	public Transform GetPlayerHolderTransform()
	{
		return playerHolder;
	}
}
