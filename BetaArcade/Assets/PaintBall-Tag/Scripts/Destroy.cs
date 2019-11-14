using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        int a = 5;
        int b = a + 7;
        int ac= 7;


        StartCoroutine(DestoryAfterTime());
    }

    IEnumerator DestoryAfterTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

}
