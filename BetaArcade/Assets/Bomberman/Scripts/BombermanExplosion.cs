using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanExplosion : MonoBehaviour
{
	[SerializeField]
	float explosionTimer = .3f;
    // Start is called before the first frame update
    void Start()
    {
		Invoke("Finish", explosionTimer);
    }
	void Finish()
	{
		Destroy(gameObject);
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
