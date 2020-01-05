using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextScript : MonoBehaviour
  {
  private TextMeshProUGUI m_RoundText;
  private TextMeshProUGUI m_ScoreText;
  private TextMeshProUGUI m_TimerText;
  // Start is called before the first frame update
  void Start()
    {
    m_RoundText = transform.Find("RoundText").GetComponent<TextMeshProUGUI>();
    m_ScoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    m_TimerText = transform.Find("TimerText").GetComponent<TextMeshProUGUI>();
    }

  public void SetRoundText(int currentRound, int maxRounds)
    {
    m_RoundText.text = "Round: " + currentRound + " of " + maxRounds;
    }

  public void SetScoreText(int score)
    {
    m_ScoreText.text = "Score to win: " + score;
    }

  public void SetTimerText(int time)
    {
    m_TimerText.text = "Time: " + time;
    }

  public void SetAll(int currentRound, int maxRounds, int scoreToWin, int maxTime)
    {
    SetRoundText(currentRound, maxRounds);
    SetScoreText(scoreToWin);
    SetTimerText(maxTime);
    }
  }
