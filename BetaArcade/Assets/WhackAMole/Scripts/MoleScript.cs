using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleScript : MonoBehaviour
{
    private bool m_IsAlive = true;
    private void Start()
    {
        StartCoroutine(AliveTime());
    }

    private void Update()
    {
        if (m_IsAlive)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, .5f, transform.position.z), Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -.5f, transform.position.z), Time.deltaTime * 2);
        }
    }

    IEnumerator AliveTime()
    {
        yield return new WaitForSeconds(5);
        m_IsAlive = false;
    }
}
