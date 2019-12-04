using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanPowerupData : MonoBehaviour
{
	[SerializeField]
	int powerupID = 1;
	[SerializeField]
	float destroyTimer = 12.5f;
	bool isCollected = false;
	public int GetPowerupID()
	{
		return powerupID;
	}
    // Start is called before the first frame update
    void Start()
    {
		Invoke("EndThis", destroyTimer);
		//powerupID = transform.parent.GetComponent<BombermanPowerups>().GetPowerup();
    }
	private void EndThis()
	{
		Destroy(gameObject);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Bomberman>() && !isCollected)
		{
			isCollected = true;
			//Destroy(transform.parent.gameObject);
			Destroy(gameObject);
		}
	}
}
