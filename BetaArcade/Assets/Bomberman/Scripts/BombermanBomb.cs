using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanBomb : MonoBehaviour
{
	[SerializeField]
	float fuse = 3.0f;
	public GameObject explosionPrefab;
	public LayerMask levelMask;
	[SerializeField] private bool hasExploded = false;
	// Start is called before the first frame update
	void Start()
    {
		Invoke("Explode", fuse);
    }

	void Explode()
	{
		hasExploded = true;
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		///makes the explosions in the cardinal directions
		StartCoroutine(CreateExplosions(Vector3.forward));
		StartCoroutine(CreateExplosions(Vector3.right));
		StartCoroutine(CreateExplosions(Vector3.back));
		StartCoroutine(CreateExplosions(Vector3.left));
		Destroy(this.gameObject, .2f);
	}

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log("TriggerDetected");
		if(!hasExploded && other.CompareTag("Explosion"))
		{
			Debug.Log("ExplosionDetected");
			CancelInvoke("Explode");
			Explode();
		}
	}

	private IEnumerator CreateExplosions(Vector3 direction)
	{
		///loop x times where x is the explosion range/power
		for (int i = 0; i<3; i++)
		{
			RaycastHit hit;
			Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), direction, out hit, i, levelMask);

			if(!hit.collider)
			{
				Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
			}
			else
			{
				break;
			}
		}
		yield return new WaitForSeconds(0.05f);
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
