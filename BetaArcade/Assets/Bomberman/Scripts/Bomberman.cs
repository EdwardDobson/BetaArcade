using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomberman : MonoBehaviour
{
	public GameObject bombPrefab;
	PlayerMove player;
	public Transform myTransform;
	public bool isDead = false;

	//add global reference here
	void Start()
	{
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
		//global point allocation
		gameObject.SetActive(false);
	}

	private void DropBomb()
	{
		if (bombPrefab)
		{
			Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x), Mathf.RoundToInt(myTransform.position.y), Mathf.RoundToInt(myTransform.position.z)), bombPrefab.transform.rotation);
		}
	}

	void Update()
	{
		if (Input.GetButtonDown("Jump" + player.ID))
		{
			DropBomb();
		}
	}
}
