using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bomberman : MonoBehaviour
{
	public BombermanRoundManager manager;
	[SerializeField]
	GameObject bombPrefab;
	PlayerMove player;
	public Transform myTransform;
	public bool isDead = false;
	[SerializeField]
	float baseRegen = 1.5f;
	[SerializeField]
	float regenRate = 0.0f;
	[SerializeField]
	float baseBombPower = 3.0f;
	[SerializeField]
	float bombPower = 0.0f;
	[SerializeField]
	float baseBombMax = 3.0f;
	[SerializeField]
	float bombMax = 0.0f;
	[SerializeField]
	float bombsRemaining = 0.0f;
	[SerializeField]
	TextMeshProUGUI powerText;

	public float GetBombPower()
	{
		Debug.Log("Bomb power is: " + bombPower);
		return bombPower;
	}
	public void AddBombPower(int power)
	{
		bombPower += power;
		UpdateUI();
	}
	public void ResetPowers()
	{
		bombMax = baseBombMax;
		bombPower = baseBombPower;
		regenRate = baseRegen;
		bombsRemaining = bombMax;
		Debug.Log("Bomb power: " + bombPower + " Base: " + baseBombMax);
		UpdateUI();
	}
	public bool GetIsDead()
	{
		return isDead;
	}
	//add global reference here
	void Start()
	{
		bombPower = baseBombPower;
		bombMax = baseBombMax;
		bombsRemaining = baseBombMax;
		regenRate = baseRegen;
		manager = FindObjectOfType<BombermanRoundManager>();
		player = GetComponent<PlayerMove>();
		switch (player.ID)
		{
			case 1:
				powerText = GameObject.Find("Player1 Text").GetComponent<TextMeshProUGUI>();
				break;
			case 2:
				powerText = GameObject.Find("Player2 Text").GetComponent<TextMeshProUGUI>();
				break;
			case 3:
				powerText = GameObject.Find("Player3 Text").GetComponent<TextMeshProUGUI>();
				break;
			case 4:
				powerText = GameObject.Find("Player4 Text").GetComponent<TextMeshProUGUI>();
				break;
			default:
				break;
		}
		UpdateUI();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Explosion"))
		{
			Debug.Log("Death Detected");
			PlayerDied(); //replace with health loss
		}
		if(other.CompareTag("Powerup"))
		{
			switch (other.GetComponent<BombermanPowerupData>().GetPowerupID())
			{
				case 0:
					bombPower += 0.5f;
					UpdateUI();
					break;
				case 1:
					bombMax += 0.5f;
					UpdateUI();
					break;
				case 2:
					if(regenRate>0.5f)
					{
						regenRate -= 0.05f;
					}
					break;
				default:
					Debug.Log("Powerup Error");
					break;
			}
		}
	}
	private void PlayerDied()
	{
		isDead = true;
		manager.PlayerDown(player.ID);
		ResetPowers();
		powerText.text = " ";
		//global point allocation
		gameObject.SetActive(false);
	}

	private void DropBomb()
	{
		if (bombPrefab)
		{
			bombsRemaining--;
			GameObject clone = Instantiate(bombPrefab, new Vector3((myTransform.position.x), (myTransform.position.y), (myTransform.position.z)), bombPrefab.transform.rotation);
			clone.GetComponent<BombermanBomb>().setBombPower(bombPower);
			Debug.Log("Bomb dropped");
		
		}
	}

	void UpdateUI()
	{
		Mathf.CeilToInt(bombPower);
		Mathf.CeilToInt(bombsRemaining);
		Mathf.CeilToInt(bombMax);
		powerText.text = "Player " + player.ID + "\nPower: " + bombPower + "\nBombs: " + bombsRemaining + "/" + bombMax;
	}

	void ClearUI()
	{
		powerText.text = " ";
	}

	void Update()
	{
        regenRate -= Time.deltaTime;
		if(regenRate<= 0.0f)
		{
			regenRate = baseRegen;
			bombsRemaining++;
			if(bombsRemaining>bombMax)
			{
				bombsRemaining = bombMax;
			}
			UpdateUI();
		}
		if (Input.GetButtonDown("Jump"+player.ID) && bombsRemaining > 0 && manager.gameStarted && Time.timeScale == 1)
		{
			Debug.Log("Bomb button got");
			DropBomb();
			UpdateUI();
		}
		if(manager.hasEnded)
		{
			ClearUI();
		}
	}
}
