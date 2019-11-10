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
  private void Start()
    {
    m_ID = LevelManagerTools.GetPlayerID(gameObject);
    m_Animator = GetComponent<Animator>();
    }

  private void Update()
    {
    if(Input.GetAxis("RT" + m_ID) != 0 && CanSwing)
      {
      StartCoroutine(SwingHammer());
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
