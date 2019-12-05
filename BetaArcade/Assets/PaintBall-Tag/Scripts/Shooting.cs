using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float AmmoSpeed = 40.0f;
    public float Damage = 10.0f;
    [SerializeField] public float BulletDelay = 2.0f;

    float tempTimer = 0.0f;

    public GameObject AmmoPrefab;
    public Transform ShootingPoint;
    private PlayerMove m_PlayerMoveScript;

    void Start()
    {
        tempTimer = BulletDelay + 0.1f;
        m_PlayerMoveScript = gameObject.transform.parent.parent.GetComponent<PlayerMove>();
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("RT" + m_PlayerMoveScript.ID) > 0.1 && tempTimer>BulletDelay)
        {
            tempTimer = 0.0f;
            ShootPaintballGun();
        }
        tempTimer += Time.deltaTime;
    }

    void ShootPaintballGun()
    {
        GameObject BulletInstance = Instantiate(AmmoPrefab, ShootingPoint.position, Quaternion.identity);
        Rigidbody BulletRigidbodyInstance = BulletInstance.GetComponent<Rigidbody>();
        BulletRigidbodyInstance.velocity = ShootingPoint.forward * AmmoSpeed;
        Physics.IgnoreCollision(BulletInstance.GetComponent<Collider>(), GetComponent<Collider>());
     
    }
}


