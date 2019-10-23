using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
  {
  public Vector3 Offset = new Vector3(0, 5, -10);
  public float SmoothTime = 2.5f;
  public float ZoomFactor = 13f;

  private Transform[] m_Players;
  void FixedUpdate()
    {
    m_Players = GameObject.FindObjectsOfType(typeof(GameObject)).Where(x => (x as GameObject).tag.ToLower().Contains("player")).Select(x => (x as GameObject).transform).ToArray();
    if(m_Players.Length > 0)
      Move();
    }

  Vector3 GetCenterPoint()
    {
    if (m_Players.Length == 1)
      return m_Players[0].position;

    var bounds = new Bounds(m_Players[0].position, Vector3.zero);

    for(int i = 0; i < m_Players.Length; i++)
      bounds.Encapsulate(m_Players[i].position);

    return bounds.center;
    }

  void Move()
    {
    var centerPoint = GetCenterPoint();
    var newPosition = centerPoint + (Offset * (1 + (GetGreatestDistance() / ZoomFactor)));

    transform.position = Vector3.Lerp(transform.position, newPosition, Time.fixedDeltaTime * SmoothTime);
    }

  float GetGreatestDistance()
    {
    if (m_Players.Length == 1)
      return 0;

    var bounds = new Bounds(m_Players[0].position, Vector3.zero);

    for(int i = 0; i < m_Players.Length; i++)
      bounds.Encapsulate(m_Players[i].position);

    return bounds.size.magnitude;
    }
  }
