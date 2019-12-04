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

	[SerializeField]
	private int objectId = 0; //determines which object can be placed, every object ID has equal weighting

	[SerializeField]
	List<GameObject> Objects = new List<GameObject>();
	[SerializeField]
	bool isRounding = false;
	[SerializeField]
	bool isActive = false;
	// Start is called before the first frame update
	private void Awake()
	{
		StartCoroutine("Spawner");
	}

	public void SetActive(bool activeState)
	{
		isActive = activeState;
	}

	IEnumerator Spawner()
	{
		while(true)
		{
			if(isActive)
			{
				objectId = Random.Range(0, Objects.Count);
				posX = Random.Range(minX, maxX);
				posY = Random.Range(minY, maxY);
				posZ = Random.Range(minZ, maxZ);
				if (isRounding)
				{
					posX = Mathf.RoundToInt(posX) + 0.5f;
					posY = Mathf.RoundToInt(posY);
					posZ = Mathf.RoundToInt(posZ) + 0.5f;
				}
				timeDelay = Random.Range(minTime, maxTime);
				spawnPosition = new Vector3(posX, posY, posZ);
				GameObject clone = Instantiate(Objects[objectId], spawnPosition, Quaternion.identity);
				clone.transform.SetParent(transform);
				yield return new WaitForSeconds(timeDelay);
			}
			
		}
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
