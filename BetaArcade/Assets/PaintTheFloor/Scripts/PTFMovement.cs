using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTFMovement : MonoBehaviour
  {
  public GameObject ShootingObject;
  public GameObject PaintBall;

  private float m_AimRotateSpeed = .3f;
  private float m_ShotPower = 10f;
  private Transform m_FirePoint;
  private bool m_CanShoot = true;
  private void Start()
    {
    m_FirePoint = ShootingObject.transform.GetChild(0);
    }
  void Update()
    {
    if (Input.GetButton("RB"))
      {
      if (ShootingObject.transform.localRotation.x > -.25f)
        {
        Debug.Log(ShootingObject.transform.localRotation.x);
        ShootingObject.transform.Rotate(Vector3.left, m_AimRotateSpeed);
        }
      }
    if (Input.GetButton("LB"))
      {
      if (ShootingObject.transform.localRotation.x < .25f)
        {
        Debug.Log(ShootingObject.transform.localRotation.x);
        ShootingObject.transform.Rotate(Vector3.left, -m_AimRotateSpeed);
        }
      }

    if (Input.GetAxis("RT") != 0)
      {
      if (m_CanShoot)
        {
        StartCoroutine(ShotDelay());
        ShootPaint();
        }
      }
    }

  private void ShootPaint()
    {
    Debug.Log("Shot paint");
    var paintBall = Instantiate(PaintBall);
    paintBall.transform.position = m_FirePoint.position;
    paintBall.GetComponent<Rigidbody>().AddForce(m_FirePoint.forward * m_ShotPower, ForceMode.Impulse);
    }

  IEnumerator ShotDelay()
    {
    m_CanShoot = false;
    yield return new WaitForSeconds(.1f);
    m_CanShoot = true;
    }
  }
