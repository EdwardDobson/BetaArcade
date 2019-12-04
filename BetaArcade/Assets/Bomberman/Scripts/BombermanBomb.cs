using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanBomb : Bomberman
{
	[SerializeField]
	float fuse = 3.0f;
	public GameObject explosionPrefab;
	public LayerMask levelMask;
	[SerializeField] private bool hasExploded = false;
    [SerializeField] private bool hasTriggered = false;
	Bomberman myParent;
    Collider thisCollider;
    MeshRenderer thisRender;
	float thisBombPower = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
		myParent = transform.parent.GetComponent<Bomberman>();
		thisBombPower = myParent.GetBombPower();
        thisRender = GetComponent<MeshRenderer>();
        thisCollider = GetComponent<Collider>();
		Invoke("Explode", fuse);
    }

	void Explode()
	{
        thisRender.enabled = false;
		hasExploded = true;
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		///makes the explosions in the cardinal directions
		StartCoroutine(CreateExplosions(Vector3.forward));
		StartCoroutine(CreateExplosions(Vector3.right));
		StartCoroutine(CreateExplosions(Vector3.back));
		StartCoroutine(CreateExplosions(Vector3.left));
		Destroy(gameObject, .2f);
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
    public void OnTriggerExit(Collider other)
    {
        if(!hasTriggered)
        {
            hasTriggered = true;
            thisCollider.isTrigger = false;
        }
    }

    private IEnumerator CreateExplosions(Vector3 direction)
	{
		///loop x times where x is the explosion range/power
		for (int i = 0; i< thisBombPower; i++)
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
