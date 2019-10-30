using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] public float AmmoSpeed = 100.0f;

    public GameObject AmmoPrefab;
    public Transform ShootingPoint;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject BulletInstance = Instantiate(AmmoPrefab, transform.position, Quaternion.identity);
            Rigidbody BulletRigidbodyInstance = BulletInstance.GetComponent<Rigidbody>();
            BulletRigidbodyInstance.AddForce(ShootingPoint.forward * AmmoSpeed);
            Debug.Log("i shoudve worked");
        }
    }
}
