using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
  {
  // Singleton setup
  public static CountdownTimer Instance { get { if (m_Instance == null) m_Instance = new GameObject().AddComponent<CountdownTimer>(); return m_Instance; } }
  private static CountdownTimer m_Instance;

  public float Timeleft;

  public GameObject CountdownPrefab;
  private GameObject m_CountdownObject;
  private TextMeshProUGUI m_Text;
  private float m_MaxTime = 3f;
  private GameObject[] m_Players;
  private bool m_IsRunning = false;

  private void Awake()
    {
    if(CountdownPrefab == null)
      {
      CountdownPrefab = Resources.Load("Prefabs/Countdown") as GameObject;
      }
    }

  public void Run()
    {
    if(!m_IsRunning)
      {
      // Stop object from destroying
      StopAllCoroutines();
      m_CountdownObject = GameObject.Instantiate(CountdownPrefab);
      m_Text = m_CountdownObject.GetComponentInChildren<TextMeshProUGUI>();
      Timeleft = m_MaxTime;
      m_Text.text = Timeleft.ToString();
      ToggleFreeze(true);
      m_IsRunning = true;
      }
    }

  private void Update()
    {
    if (m_IsRunning)
      {
      Timeleft -= Time.deltaTime;
      if (Timeleft >= 0)
        {
        m_Text.text = Mathf.CeilToInt(Timeleft).ToString();
        }
      else
        {
        ToggleFreeze(false);
        StartCoroutine(DestroyCountdown());
        }
      }
    }

  private void ToggleFreeze(bool isFrozen)
    {
    m_Players = GameObject.FindObjectsOfType<GameObject>().Where(x => x.tag.Contains("Player")).ToArray();
    foreach (var player in m_Players)
      {
      bool usePlayerMove = player.GetComponent<PlayerMove>() != null;
      if (usePlayerMove == true)
        player.GetComponent<PlayerMove>().ToggleFreeze(isFrozen);
      else
        player.GetComponent<PlayerManager>().ToggleFreeze(isFrozen);
      }
    }

  private IEnumerator DestroyCountdown()
    {
    m_Text.text = "Go!";
    yield return new WaitForSeconds(1);
    m_Text.text = "";
    m_IsRunning = false;
    yield return new WaitForSeconds(3);
    Destroy(m_CountdownObject);
    if(m_Instance != null)
      Destroy(m_Instance.gameObject);
    }
  }
