using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCollisions : MonoBehaviour
{
  public GameObject Hammer;
  public void DisableHammerCollider() => Hammer.GetComponentInChildren<BoxCollider>().enabled = false;
  public void EnableHammerCollider() => Hammer.GetComponentInChildren<BoxCollider>().enabled = true;
  }
