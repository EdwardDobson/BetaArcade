using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
  {
  private enum Direction { Up, Right, Down, Left};

  public GameObject TrailObject;
  public int ID;

  private Rigidbody m_Rb;
  [SerializeField]
  private Direction m_Direction;
  private float m_OriginalSpeed = 0.2f;
  private float m_Speed = 0.2f;
  private bool m_IsDead;
  private float m_TrailTime = 2f;
  private GameObject m_TrailParent;
  private void Start()
    {
    m_Rb = GetComponent<Rigidbody>();
    m_Direction = Direction.Up;
    m_TrailParent = new GameObject(ID + " trail");
    }

  private void Update()
    {
    float h = Input.GetAxisRaw("Horizontal" + ID);
    float v = Input.GetAxisRaw("Vertical" + ID);
    if (h < -.75f && m_Direction != Direction.Right)
      m_Direction = Direction.Left;
    else if (h > .75f && m_Direction != Direction.Left)
      m_Direction = Direction.Right;
    else if (v < -.75f && m_Direction != Direction.Up)
      m_Direction = Direction.Down;
    else if (v > .75f && m_Direction != Direction.Down)
      m_Direction = Direction.Up;

    }

  void LateUpdate()
    {
    if (!m_IsDead)
      {
      var trail = Instantiate(TrailObject);
      var trailScript = trail.GetComponent<TrailScript>();
      trail.transform.position = transform.position;
      trail.transform.SetParent(m_TrailParent.transform);
      trail.GetComponent<Renderer>().material.SetColor("_BaseColor", GetComponent<Renderer>().material.GetColor("_BaseColor"));
      trailScript.ID = ID;

      if(Input.GetButton("Dash" + ID))
        m_Rb.MovePosition(transform.position + (DirectionToVector3(m_Direction) * 2));
      else
        m_Rb.MovePosition(transform.position + DirectionToVector3(m_Direction));

      //transform.position += DirectionToVector3(m_Direction);
      transform.eulerAngles = new Vector3(0, 90 * (int)m_Direction, 0);
      StartCoroutine(DeleteAfterTime(trail));
      }
    }
  public void Die()
    {
    m_IsDead = true;

    // TODO could do some nice death fade and still show previous player on map
    #region DeathFade
    //StopAllCoroutines();
    #endregion
    Destroy(m_TrailParent);
    Destroy(gameObject);
    }

  public void SetWinner()
    {
    m_IsDead = true;
    // TODO?
    }

  IEnumerator DeleteAfterTime(GameObject trail)
    {
    yield return new WaitForSeconds(m_TrailTime);
    Destroy(trail);
    }
  private void OnCollisionEnter(Collision collision)
    {
    Die();
    }
  public void Speedup()
    {
    StartCoroutine(SpeedupPowerup());
    }

  IEnumerator SpeedupPowerup()
    {
    m_OriginalSpeed *= 1.25f;
    m_Speed = m_OriginalSpeed;
    yield return new WaitForSeconds(5);
    m_OriginalSpeed /= 1.25f;
    m_Speed = m_OriginalSpeed;
    }
  Vector3 DirectionToVector3(Direction dir)
    {
    switch (dir)
      {
      case Direction.Up:
        return new Vector3(0, 0,m_Speed);
      case Direction.Right:
        return new Vector3(m_Speed, 0);
      case Direction.Down:
        return new Vector3(0, 0, -m_Speed);
      case Direction.Left:
        return new Vector3(-m_Speed, 0);
      default:
        return Vector3.zero;
      }
    }
  }
