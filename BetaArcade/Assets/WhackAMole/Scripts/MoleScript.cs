using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleScript : MonoBehaviour
  {
  private System.Random m_Random = new System.Random(System.DateTime.Now.Millisecond);
  private void Start()
    {
    GetComponent<Animator>().speed = m_Random.Next(50, 100) / 100.0f;
    GetComponent<Animator>().SetTrigger("ComeUp");
    StartCoroutine(AliveTime());
    }

  IEnumerator AliveTime()
    {
    yield return new WaitForSeconds(m_Random.Next(50, 500) / 100.0f);
    GetComponent<Animator>().SetTrigger("ComeDown");
    }

  public void Destroy()
    {
    GameObject.Find("LevelManager").GetComponent<WAMLevelManager>().MoleCount--;
    Destroy(gameObject);
    }
  }
