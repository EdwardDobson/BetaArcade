using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TronIncreaseSpeed : MonoBehaviour
  {
  private void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.tag.Contains("Player"))
      {
      GameObject Clone = GameObject.Find("PlayerPicture" + LevelManagerTools.GetPlayerID(other.gameObject));
      other.GetComponent<PlayerManager>().Speedup();
      if(Clone != null)
        {
        Clone.transform.GetChild(other.gameObject.GetComponent<PlayerMove>().GetPowerUpCount()).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        Clone.transform.GetChild(other.gameObject.GetComponent<PlayerMove>().GetPowerUpCount()).name = name;
        }
      Destroy(gameObject);
      }
    }
  }
