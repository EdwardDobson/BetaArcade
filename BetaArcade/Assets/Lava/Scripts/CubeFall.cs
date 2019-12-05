using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFall : MonoBehaviour
{

    private Vector3 startPos;
    private Vector3 endPos;
    private bool falling;
    public float speed;
    public float fallWait;
    public float respawn;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (falling == true)
        {
            StartCoroutine("waitplz");
        } else
        {
            StartCoroutine("reverseplz");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            falling = true;
        }
    }

    IEnumerator waitplz()
    {
        yield return new WaitForSeconds(fallWait);
        fall();
        falling = false;
    }
    IEnumerator reverseplz()
    {
        yield return new WaitForSeconds(respawn);
        reverse();
    }

    void fall()
    {
        transform.position = Vector3.Lerp(transform.position, endPos, speed * Time.deltaTime);
    }

    void reverse()
    {
        transform.position = Vector3.Lerp(transform.position, startPos, speed * Time.deltaTime);
    }
}
