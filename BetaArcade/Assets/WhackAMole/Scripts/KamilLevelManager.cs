﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class KamilLevelManager : MonoBehaviour
  {
  private int m_CurrentRound;

  protected int m_MaxRounds = 1;
  protected int CurrentRound
    {
    get { return m_CurrentRound; }
    set
      {
      m_CurrentRound = value;
      Debug.Log("Round Set");
      m_UITextScript.SetRoundText(value, m_MaxRounds);
      }
    }
  protected bool m_IsPaused = false;
  protected GameManager m_GameManager;
  protected UITextScript m_UITextScript;
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
    m_UITextScript = GameObject.Find("MainCanvas") != null ? GameObject.Find("MainCanvas").GetComponent<UITextScript>() : null;
    if (m_GameManager != null)
      {
      m_Timer = m_GameManager.GetTimer();
      m_OldTimer = m_Timer;
      }
    TargetPlayers = LevelManagerTools.GetLevelInfo(out m_MaxRounds);
    if(m_UITextScript != null)
      {
      m_UITextScript.SetAll(m_CurrentRound, m_MaxRounds, 1, m_GameManager.GetTimer());
      }
    }

  protected bool LevelCheck()
    {
    if(m_CurrentRound >= m_MaxRounds)
      {
      // TODO Maybe wait a little bit
      var portraits = GameObject.Find("PlayerPortraits");
      if(portraits != null)
        {
        foreach (Transform child in portraits.transform)
          {
          var score = child.Find("Score");
          if (score != null)
            {
            score.GetComponent<TextMeshProUGUI>().text = "";
            }
          }
        }
      m_GameManager.transform.GetChild(0).gameObject.SetActive(true);
      m_UITextScript.gameObject.SetActive(false);
      return true;
      }
    return false;
    }

  protected void HandleTimer()
    {
    m_Timer = Mathf.Max(m_Timer - Time.deltaTime, 0.0f);
    m_UITextScript.SetTimerText((int)m_Timer);
    if (m_Timer <= 0.0f)
      StartCoroutine(EndRound());
    }
  }
