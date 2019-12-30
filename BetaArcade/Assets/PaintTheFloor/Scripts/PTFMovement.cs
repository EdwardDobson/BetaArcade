using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PTFMovement : MonoBehaviour
  {
  public GameObject ShootingObject;
  public GameObject PaintBall;

  public AudioClip ShotSound;

  public bool IsPaused = true;
  public int Score = 0;

  private float m_AimRotateSpeed = .3f;
  private float m_ShotPower = 10f;
  private Transform m_FirePoint;
  private bool m_CanShoot = true;
  private PlayerMove m_PlayerMoveScript;
  private float m_FireRate = 7.5f;
  private float m_ShotSize = 1;
  private Animator m_CharacterAnimator;

  public float FireRate
    {
    get { return m_FireRate; }
    set
      {
      m_FireRate = value;
      StartCoroutine(ResetFireRate());
      }
    }
  public float ShotSize
    {
    get { return m_ShotSize; }
    set
      {
      m_ShotSize = value;
      StartCoroutine(ResetShotSize());
      }
    }
  private void Start()
    {
    m_PlayerMoveScript = gameObject.GetComponent<PlayerMove>();
    m_CharacterAnimator = GetComponent<Animator>();
    m_FirePoint = transform.Find("FirePoint");
    }
  void Update()
    {
    m_CharacterAnimator.SetFloat("MoveSpeed", GetComponent<Rigidbody>().velocity.magnitude >= 0.5f ? GetComponent<Rigidbody>().velocity.magnitude : 0);
    if (!IsPaused)
      {
      if (Input.GetButton("RB" + m_PlayerMoveScript.ID))
        {
        if (ShootingObject.transform.localRotation.x > -.25f)
          {
          Debug.Log(ShootingObject.transform.localRotation.x);
          ShootingObject.transform.Rotate(Vector3.left, m_AimRotateSpeed);
          }
        }
      if (Input.GetButton("LB" + m_PlayerMoveScript.ID))
        {
        if (ShootingObject.transform.localRotation.x < .25f)
          {
          Debug.Log(ShootingObject.transform.localRotation.x);
          ShootingObject.transform.Rotate(Vector3.left, -m_AimRotateSpeed);
          }
        }

      if (Input.GetAxis("RT" + m_PlayerMoveScript.ID) != 0)
        {
        if (m_CanShoot)
          {
          m_CharacterAnimator.SetBool("IsShooting", true);
          StartCoroutine(ShotDelay());
          ShootPaint();
          }
        }
      else
        m_CharacterAnimator.SetBool("IsShooting", false);
      }
    }

  private void ShootPaint()
    {
    //var paintBall = Instantiate(PaintBall);
    //paintBall.GetComponent<PaintballScript>().Color = GetComponent<Renderer>().material.GetColor("_BaseColor");
    //paintBall.transform.position = m_FirePoint.position;
    //paintBall.GetComponent<Rigidbody>().AddForce(m_FirePoint.forward * m_ShotPower, ForceMode.Impulse);
    //paintBall.transform.localScale *= ShotSize;
    GetComponent<AudioSource>().PlayOneShot(ShotSound);
    }

  IEnumerator ShotDelay()
    {
    m_CanShoot = false;
    yield return new WaitForSeconds(1 / FireRate);
    m_CanShoot = true;
    }
  IEnumerator ResetShotSize()
    {
    yield return new WaitForSeconds(5);
    GameObject Clone = GameObject.Find("PlayerPicture" + m_PlayerMoveScript.ID);
    if (Clone != null)
      {
      foreach (Transform t in Clone.transform)
        {
        if (t.name == "PUFirerate")
          {
          t.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
          }
        }
      }

    m_PlayerMoveScript.DecreasePowerUpCount();
    m_ShotSize = 1;
    }
  IEnumerator ResetFireRate()
    {
    yield return new WaitForSeconds(5);
    GameObject Clone = GameObject.Find("PlayerPicture" + m_PlayerMoveScript.ID);
    if(Clone != null)
      {
      foreach (Transform t in Clone.transform.transform)
        {
        if (t.name == "PUShotsize")
          {
          t.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
          }
        }
      }
    m_PlayerMoveScript.DecreasePowerUpCount();
    m_FireRate = 7.5f;
    }
  }
