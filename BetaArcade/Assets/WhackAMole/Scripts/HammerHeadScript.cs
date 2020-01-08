using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHeadScript : MonoBehaviour
  {
  public AudioClip BonkEffect;
  private void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.name == "Mole")
      {
      Destroy(other.gameObject);
      GetComponentInParent<WAMPlayerManager>().Score++;
      GameObject.Find("LevelManager").GetComponent<WAMLevelManager>().MoleCount--;
      GetComponent<AudioSource>().PlayOneShot(BonkEffect);
      }
    else if (other.gameObject.tag.Contains("Player"))
      {
      other.GetComponent<WAMPlayerManager>().Stun();
      }
    }
  }
