using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanPowerups : MonoBehaviour
{
	[SerializeField]
	int powerup = 0;
	[SerializeField]
	List<GameObject> powerups = new List<GameObject>();
	// Start is called before the first frame update
	void Start()
    {
		powerup = Random.Range(0, powerups.Count);
		DeterminePowerup();
    }
	public int GetPowerup()
	{
		return powerup;
	}
	void DeterminePowerup()
	{
		switch (powerup)
		{
			case 0:
				break;
			case 1:
				break;
			case 2:
				break;
			default:
				Debug.LogError("Powerup Error");
				break;
		}
		GameObject clone = Instantiate(powerups[powerup], transform.position, Quaternion.identity);
		clone.transform.SetParent(transform);
	}
}
