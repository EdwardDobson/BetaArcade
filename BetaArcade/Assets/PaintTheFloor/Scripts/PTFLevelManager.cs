using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTFLevelManager : KamilLevelManager
  {
  public GameObject Player;
  public GameObject FlatFloor;

  private int maxX = 50;
  private int maxY = 50;

  protected override void Start()
    {
    base.Start();
    FlatFloor.transform.localScale = new Vector3(maxX, .1f, maxY);
    Physics.IgnoreLayerCollision(9, 10);

    #region Level Generation
    var levelParent = new GameObject("levelParent");
    for (int i = 0; i <= maxX; i++)
      {
      for (int y = 0; y <= maxY; y++)
        {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(i - (maxX / 2), 0, y - (maxY / 2));
        cube.transform.localScale = new Vector3(1, 0.2f, 1);
        cube.transform.SetParent(levelParent.transform);
        }
      }
    #endregion

    CountdownTimer.Instance.Run();
    m_IsPaused = true;
    }

  private void Update()
    {
    // DEBUG
    if (Debug.isDebugBuild)
      {
      if (Input.GetKeyDown(KeyCode.P) && m_Players.Count < 4)
        CreatePlayer();
      }

    if (!m_IsPaused)
      {
      m_Timer -= Time.deltaTime;
      if (m_Timer <= 0)
        {
        StartCoroutine(EndRound());
        }
      }
    else
      {
      if (CountdownTimer.Instance.Timeleft <= 0)
        m_IsPaused = false;
      }
    }

  protected override void CreatePlayer()
    {
    var player = Instantiate(Player);
    player.transform.position = new Vector3(5 * m_Players.Count, .8f, 0);
    m_Players.Add(player);
    player.tag = "Player" + m_Players.Count;
    player.GetComponent<Renderer>().material.SetColor("_BaseColor", LevelManagerTools.PlayerIDToColor(m_Players.Count));
    player.GetComponent<PlayerMove>().ID = m_Players.Count;
    }

  public override IEnumerator EndRound()
    {
    yield return new WaitForSeconds(2);
    LevelCheck();
    m_CurrentRound++;
    }
  }
