using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
	[SerializeField]
	private float maxX; //edit max values depending on the size of the arena/spawn zone for hazards
	float posX = 0.0f;
	[SerializeField]
	private float maxZ;
	float posZ = 0.0f;
	float timeDelay = 0.0f; //time delay of spawning next bomb
	Vector3 spawnPosition;
	public GameObject Bomb;
	// Start is called before the first frame update
	private void Awake()
	{
		StartCoroutine("Spawner");
	}

	IEnumerator Spawner()
	{
		while(true)
		{
			posX = Random.Range(0.0f, maxX);
			posZ = Random.Range(0.0f, maxZ);
			timeDelay = Random.Range(0.0f, 4.0f);
			spawnPosition = new Vector3(posX, transform.position.y, posZ);
			Instantiate(Bomb, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(timeDelay);
		}
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
