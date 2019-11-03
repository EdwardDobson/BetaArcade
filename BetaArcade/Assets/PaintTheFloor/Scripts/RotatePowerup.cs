using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerup : MonoBehaviour
{
    System.Random rand;
    int x;
    int y;
    int z;
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random(GetInstanceID());
        x = rand.Next(0, 360);
        y = rand.Next(0, 360);
        z = rand.Next(0, 360);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(GetComponent<Renderer>().bounds.center, new Vector3(rand.Next(0, 360), rand.Next(0, 360), rand.Next(0, 360)), 90 * Time.deltaTime);
        transform.Rotate(new Vector3(x,y, z), Time.deltaTime * 100);
    }
}
