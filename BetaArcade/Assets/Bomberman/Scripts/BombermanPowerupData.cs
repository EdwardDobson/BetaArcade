using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanPowerupData : MonoBehaviour
{
	int powerupID = 0;
	public int GetPowerupID()
	{
		return powerupID;
	}
    // Start is called before the first frame update
    void Start()
    {
		powerupID = transform.parent.GetComponent<BombermanPowerups>().GetPowerup();
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Bomberman>())
		{
			Destroy(transform.parent.gameObject);
			Destroy(this.gameObject);
		}
	}
}
