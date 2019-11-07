using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomberman : MonoBehaviour
{
	public BombermanRoundManager manager;
	public GameObject bombPrefab;
	PlayerMove player;
	public Transform myTransform;
	public bool isDead = false;
	[SerializeField]
	float baseCooldown = 1.5f;
	float cooldown = 0.0f;

	//add global reference here
	void Start()
	{
		cooldown = baseCooldown;
		player = GetComponent<PlayerMove>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Explosion"))
		{
			Debug.Log("Death Detected");
			PlayerDied(); //replace with health loss
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
			Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x) + 0.5f, Mathf.RoundToInt(myTransform.position.y), Mathf.RoundToInt(myTransform.position.z) + 0.5f), bombPrefab.transform.rotation);
		}
	}

	void Update()
	{
        cooldown -= Time.deltaTime;
		if (Input.GetButtonDown("Jump"+player.ID) && cooldown <= 0)
		{
			Debug.Log("Bomb button got");
			DropBomb();
		}
	}
}
