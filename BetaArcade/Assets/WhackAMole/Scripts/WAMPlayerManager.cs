using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAMPlayerManager : MonoBehaviour
  {
  public GameObject Hammer;
  public bool CanSwing = true;
  public int Score
    {
    get { return m_Score; }
    set
      {
      m_Score = value;
      if(m_Score >= m_MaxScore)
        {
        GameObject.Find("LevelManager").GetComponent<WAMLevelManager>().EndRound();
        }
      }
    }

  private int m_Score;
  private int m_MaxScore = 20;
  private int m_ID;
  private float m_SwingCooldownTime = 1f;
  private Animator m_Animator;
  private GameManager m_GameManager;
  private Animator m_CharacterAnimator;
  private void Start()
    {
    m_ID = LevelManagerTools.GetPlayerID(gameObject);
    m_Animator = GetComponent<Animator>();
    m_GameManager = GameObject.Find("GameManager") != null ? GameObject.Find("GameManager").GetComponent<GameManager>() : null;
    m_CharacterAnimator = GetComponentsInChildren<Animator>()[1];//.SetFloat("MoveSpeed", GetComponent<Rigidbody>().velocity.magnitude);
    }

  private void Update()
    {
    if(Input.GetAxis("RT" + m_ID) != 0 && CanSwing)
      {
      StartCoroutine(SwingHammer());
      }

    if(m_CharacterAnimator != null)
      {
      m_CharacterAnimator.SetFloat("MoveSpeed", GetComponent<Rigidbody>().velocity.magnitude);
      m_CharacterAnimator.speed = GetComponent<Rigidbody>().velocity.magnitude <= 0.1 ? 1 : Mathf.Abs(GetComponent<Rigidbody>().velocity.magnitude) / 5;
      }
    }

  public void DisableHammerCollider() => Hammer.GetComponentInChildren<BoxCollider>().enabled = false;
  public void EnableHammerCollider() => Hammer.GetComponentInChildren<BoxCollider>().enabled = true;

  public void Stun() => StartCoroutine(StunRoutine());

  private IEnumerator StunRoutine()
    {
    CanSwing = false;
    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    yield return new WaitForSeconds(2f);
    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    CanSwing = true;
    }

  private IEnumerator SwingHammer()
    {
    CanSwing = false;
    m_Animator.SetTrigger("CanSwing");
    yield return new WaitForSeconds(m_SwingCooldownTime);
    CanSwing = true;
    }
  }
