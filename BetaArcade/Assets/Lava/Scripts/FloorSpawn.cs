using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawn : MonoBehaviour
{

    public GameObject[] floor;
    public int gridX;
    public int gridZ;
    public float offset = 1f;
    public Vector3 origin = Vector3.zero;

    

    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn()
    {
        for(int x = 0; x < gridX; x++)
        {
            for(int z = 0; z < gridZ; z++)
            {
                Vector3 spawnPos = new Vector3(x * offset, 0, z * offset) + origin;
                CubeSpawn(spawnPos, Quaternion.identity);
            }
        }
    }

    void CubeSpawn(Vector3 spawnPos, Quaternion spawnRotation)
    {
        int randomIndex = Random.Range(0, floor.Length);
        Object clone = Instantiate(floor[randomIndex], spawnPos, spawnRotation);
    }
}
