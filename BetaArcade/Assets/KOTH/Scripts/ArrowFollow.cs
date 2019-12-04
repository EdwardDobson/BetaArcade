using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFollow : MonoBehaviour
{
    public GameObject Arrow;
    public Transform Target;
    public GameObject Point;
    // Start is called before the first frame update
    void Start()
    {
        Point = GameObject.Find("Point");
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).transform.LookAt(Point.GetComponent<Renderer>().bounds.center, Vector3.left);
    }
}
