using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTFLevelManager : MonoBehaviour
  {
  public GameObject FlatFloor;
  private int maxX = 50;
  private int maxY = 50;
  void Start()
    {
    FlatFloor.transform.localScale = new Vector3(maxX, .1f, maxY);
    Physics.IgnoreLayerCollision(9, 10);

    #region Level Generation
    for (int i = 0; i <= maxX; i++)
      {
      for (int y = 0; y <= maxY; y++)
        {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(i - (maxX / 2), 0, y - (maxY / 2));
        cube.transform.localScale = new Vector3(1, 0.2f, 1);
        }
      }
    #endregion
    }
  }
