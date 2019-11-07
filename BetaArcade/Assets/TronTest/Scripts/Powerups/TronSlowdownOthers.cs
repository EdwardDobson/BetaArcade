using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TronSlowdownOthers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            List<GameObject> players = GameObject.FindObjectsOfType<GameObject>().Where(x => x.tag.Contains("Player")).ToList();
            foreach(var player in players)
            {
                if(player != other.gameObject)
                {
                    player.GetComponent<PlayerManager>().Slowdown();
                }
            }

            GameObject Clone = GameObject.Find("PlayerPicture" + LevelManagerTools.GetPlayerID(other.gameObject));
            if (Clone != null)
            {
                Clone.transform.GetChild(other.gameObject.GetComponent<PlayerMove>().GetPowerUpCount()).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                Clone.transform.GetChild(other.gameObject.GetComponent<PlayerMove>().GetPowerUpCount()).name = name;
            }
            Destroy(gameObject);
        }
    }
}
