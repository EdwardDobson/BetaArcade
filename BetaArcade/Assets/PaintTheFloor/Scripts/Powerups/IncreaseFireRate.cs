using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IncreaseFireRate : MonoBehaviour
  {
  public Sprite Icon;
  private void OnTriggerEnter(Collider other)
    {
    if (other.tag.Contains("Player"))
      {
      GameObject Clone = GameObject.Find("PlayerPicture" + other.gameObject.GetComponent<PlayerMove>().ID);
      other.gameObject.GetComponent<PTFMovement>().FireRate *= 2;
      other.gameObject.GetComponent<PlayerMove>().IncreasePowerUpCount(1);
      if (Clone != null)
        {
        Clone.transform.GetChild(other.gameObject.GetComponent<PlayerMove>().GetPowerUpCount()).GetComponent<Image>().sprite = Icon;
        Clone.transform.GetChild(other.gameObject.GetComponent<PlayerMove>().GetPowerUpCount()).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        Clone.transform.GetChild(other.gameObject.GetComponent<PlayerMove>().GetPowerUpCount()).name = name;
        }
        
      Destroy(gameObject);
      }
    }
  }
