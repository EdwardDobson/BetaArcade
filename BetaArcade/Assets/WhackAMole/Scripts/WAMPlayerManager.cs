using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAMPlayerManager : MonoBehaviour
  {
  public GameObject Hammer;

  private int m_ID;
  private float m_SwingCooldownTime = 5f;
  private bool m_CanSwing = true;
  private void Start()
    {
    m_ID = LevelManagerTools.GetPlayerID(gameObject);
    }

  private void Update()
    {
    if(Input.GetAxis("RT" + m_ID) != 0 && m_CanSwing)
      {
      StartCoroutine(SwingHammer());
      }
    }

  private IEnumerator SwingHammer()
    {
    // TODO swing
    m_CanSwing = false;
    yield return new WaitForSeconds(m_SwingCooldownTime);
    m_CanSwing = true;
    }
  }
