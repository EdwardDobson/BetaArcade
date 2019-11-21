using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killed : MonoBehaviour
{
    public float Health = 50.0f;
    private Shooting ShootingScript;

    void Start()
    {
       ShootingScript = GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        ShootingScript = GetComponentsInChildren<Transform>().Where(x => x.gameObject.activeSelf).First(x => x.GetComponent<Shooting>() != null).GetComponent<Shooting>();

        if (col.gameObject.tag == "Bullet")
        {
            Health -= ShootingScript.Damage;
            Debug.Log("i did damage");
            Destroy(col.gameObject);
        }
    }

}
