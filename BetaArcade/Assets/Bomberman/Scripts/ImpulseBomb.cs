﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseBomb : MonoBehaviour
{
	public GameObject bomb;
	public float radius = 5.0F;
	public float power = 15.0F;
	public float heightForce = 1.0f; //used to make things go higher

	[SerializeField]
	private float fuseTimer = 5.0f;

	void Start()
	{

		Invoke("Explosion", fuseTimer);
	}

	void FixedUpdate()
	{

	}

	void Explosion()
	{
		Vector3 explosionPos = bomb.transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius); //will check for valid objects in a specified radius to feed into the loop below
		foreach (Collider hit in colliders)
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			Debug.Log("Found collider");
			if (rb != null)
			{
				rb.AddExplosionForce(power, explosionPos, radius, heightForce, ForceMode.VelocityChange);
			}
		}
		Invoke("Finished", 0.5f);
	}
	void Finished()
	{
		Destroy(gameObject);
	}

}
