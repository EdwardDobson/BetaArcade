﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
	[SerializeField]
	float lavaTime = 120.0f; //replace with global timer
	bool hasStopped = false;
	float riseRate = 0.0f; //set default
	[SerializeField]
	float defaultRiseRate = 0.1f; //could be influenced by a difficulty option?
								  // Start is called before the first frame update
	void Start()
	{
		riseRate = defaultRiseRate;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
		{
			Debug.Log("Hit Player");
			//call function inside player to kill it or something
		}
	}

	IEnumerator SpawnPlatforms()
	{
		while(true)
		{
			yield return new WaitForSeconds (5.0f);
		}
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		lavaTime -= Time.deltaTime;

		if (transform.position.y <1)
		{
			Debug.Log("Called 1");
			riseRate = defaultRiseRate * 1;
		}
		else if (transform.position.y < 3)
		{
			Debug.Log("Called 2");
			riseRate = defaultRiseRate * 2;
		}
		else if (transform.position.y < 6)
		{
			Debug.Log("Called 3");
			riseRate = defaultRiseRate * 3;
		}
		else if (transform.position.y >= 6 && !hasStopped)
		{
			hasStopped = true;
			riseRate = defaultRiseRate * 0;
			StartCoroutine("SpawnPlatforms");
		}
		else
		{
			Debug.Log("Time up/ERROR");
		}
		
		transform.position += Vector3.up * riseRate;
	}
}



