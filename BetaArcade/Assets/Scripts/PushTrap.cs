using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Contains("Player"))
        {
            Vector3 pushPos = other.transform.position;
            Collider[] colliders = Physics.OverlapBox(pushPos, transform.localScale / 4);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(100, pushPos, 5, 1.0f);
                }
            }
        }
    }
}
