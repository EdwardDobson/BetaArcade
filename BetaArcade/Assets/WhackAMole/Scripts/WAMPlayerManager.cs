using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAMPlayerManager : MonoBehaviour
  {
  public GameObject Hammer;
  public bool CanSwing = true;
  public int Score = 0;

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

  private IEnumerator SwingHammer()
    {
    CanSwing = false;
    m_Animator.SetTrigger("CanSwing");
    yield return new WaitForSeconds(m_SwingCooldownTime);
    CanSwing = true;
    }
  }
