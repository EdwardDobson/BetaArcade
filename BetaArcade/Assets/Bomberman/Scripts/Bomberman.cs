using System.Collections;
using System.Collections.Generic;
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
	int baseBombPower = 3;
	[SerializeField]
	int bombPower = 0;
	[SerializeField]
	int baseBombNumber = 3;
	[SerializeField]
	int bombNumber = 0;
	[SerializeField]
	int bombsRemaining = 0;

	public int GetBombPower()
	{
		Debug.Log("Bomb power is: " + bombPower);
		return bombPower;
	}
	public void AddBombPower(int power)
	{
		bombPower += power;
	}
	//add global reference here
	void Start()
	{
		bombPower = baseBombPower;
		bombNumber = baseBombNumber;
		bombsRemaining = baseBombNumber;
		regenRate = baseRegen;
		player = GetComponent<PlayerMove>();
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
					bombPower++;
					break;
				case 1:
					bombNumber++;
					break;
				case 2:
					if(regenRate>0.5f)
					{
						regenRate -= 0.1f;
					}
					break;
				default:
					Debug.Log("Powerup Error");
					break;
			}
		}
	}
	private void PlayerDied() //will probably need player id from global or something
	{
		isDead = true;
		manager.PlayerDown();
		//global point allocation
		gameObject.SetActive(false);
	}

	private void DropBomb()
	{
		if (bombPrefab)
		{
			bombsRemaining--;
			GameObject clone = Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x) + 0.5f, Mathf.RoundToInt(myTransform.position.y), Mathf.RoundToInt(myTransform.position.z) - 0.5f), bombPrefab.transform.rotation);
			clone.transform.SetParent(transform);
			Debug.Log("Bomb dropped");
		}
	}

	void Update()
	{
        regenRate -= Time.deltaTime;
		if(regenRate<= 0.0f)
		{
			regenRate = baseRegen;
			bombsRemaining++;
			if(bombsRemaining>bombNumber)
			{
				bombNumber = bombsRemaining;
			}
		}
		if (Input.GetButtonDown("Jump"+player.ID) && bombsRemaining > 0)
		{
			Debug.Log("Bomb button got");
			DropBomb();
		}
	}
}
