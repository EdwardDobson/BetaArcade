using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
	[SerializeField]
	private float maxX = 0.0f; //edit max values depending on the size of the arena/spawn zone for hazards
	[SerializeField]
	private float minX = 0.0f;
	private float posX = 0.0f;

	[SerializeField]
	private float maxY = 0.0f;
	[SerializeField]
	private float minY = 0.0f;
	private float posY = 0.0f;

	[SerializeField]
	private float maxZ = 0.0f;
	[SerializeField]
	private float minZ = 0.0f;
	private float posZ = 0.0f;

	[SerializeField]
	private float minTime = 0.0f;
	[SerializeField]
	private float maxTime = 0.0f;
	float timeDelay = 0.0f; //time delay of spawning next bomb
	Vector3 spawnPosition;

	public GameObject Object;
	// Start is called before the first frame update
	private void Awake()
	{
		StartCoroutine("Spawner");
	}

	IEnumerator Spawner()
	{
		while(true)
		{
			posX = Random.Range(minX, maxX);
			posY = Random.Range(minY, maxY);
			posZ = Random.Range(minZ, maxZ);
			timeDelay = Random.Range(minTime, maxTime);
			spawnPosition = new Vector3(posX, posY, posZ);
			GameObject clone = Instantiate(Object, spawnPosition, Quaternion.identity);
			clone.transform.SetParent(transform);
			yield return new WaitForSeconds(timeDelay);
		}
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
