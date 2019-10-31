using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerup : MonoBehaviour
  {
  System.Random rand;
  // Start is called before the first frame update
  void Start()
    {
    int seed = 0;
    rand = new System.Random(seed);
    }

  // Update is called once per frame
  void Update()
    {
    transform.RotateAround(GetComponent<Renderer>().bounds.center, new Vector3(rand.Next(0, 360), rand.Next(0, 360), rand.Next(0, 360)), 90 * Time.deltaTime);
    }
  }
