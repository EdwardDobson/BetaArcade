using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public float platformLifetime;
    // Start is called before the first frame update
    void Awake()
    {
		Invoke("PlatformDelete", platformLifetime);
    }
	void PlatformDelete()
	{
		Destroy(gameObject);
	}
}
