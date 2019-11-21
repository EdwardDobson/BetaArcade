using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBTLevManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject SpawnPointParent;
    private List <GameObject> SpawnPoints = new List<GameObject>(); //list

    private int PlayerCount = 0;


    void Start()
    {
        foreach (Transform SP in SpawnPointParent.transform)
        {
            SpawnPoints.Add(SP.gameObject);
        }

        for(int i = 0; i < 4; i++)
        {
            PlayerCreate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerCreate()
    {
        var player = GameObject.Instantiate(PlayerPrefab);
        player.transform.position = SpawnPoints[PlayerCount].transform.position;
        player.transform.SetParent(transform);
        var Movescript = player.GetComponent<PlayerMove>();
        PlayerCount++;
        Movescript.ID = PlayerCount;
        var WeaponScript = player.GetComponentInChildren<WeaponSwitching>();
        WeaponScript.ID = PlayerCount;
        //player.GetComponent<Renderer>().material.SetColor("_BaseColor", LevelManagerTools.PlayerIDToColor(PlayerCount));
    }
}
