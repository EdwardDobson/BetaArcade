using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class KamilLevelManager : MonoBehaviour
  {
  protected int m_MaxRounds = 1;
  protected int m_CurrentRound = 0;
  protected bool m_IsPaused = false;
  protected GameManager m_GameManager;
  [SerializeField]
  protected float m_Timer;
  protected float m_OldTimer;

  protected List<GameObject> m_Players = new List<GameObject>();
  protected abstract void CreatePlayer();
  public abstract IEnumerator EndRound();
  public int TargetPlayers
    {
    set
      {
      for (int i = m_Players.Count; i < value; i++)
        {
        CreatePlayer();
        }
      }
    }

  protected virtual void Start()
    {
    m_GameManager = GameObject.Find("GameManager") != null ? GameObject.Find("GameManager").GetComponent<GameManager>() : null;
    if(m_GameManager != null)
      {
      m_Timer = m_GameManager.GetTimer();
      m_OldTimer = m_Timer;
      }
    TargetPlayers = LevelManagerTools.GetLevelInfo(out m_MaxRounds);
    }

  protected bool LevelCheck()
    {
    if(m_CurrentRound >= m_MaxRounds)
      {
      // TODO Maybe wait a little bit
      m_GameManager.transform.GetChild(0).gameObject.SetActive(true);
      return true;
      }
    return false;
    }

  protected void HandleTimer()
    {
    m_Timer = Mathf.Max(m_Timer - Time.deltaTime, 0.0f);
    if (m_Timer <= 0.0f)
      EndRound();
    }
  }
