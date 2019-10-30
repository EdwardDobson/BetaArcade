using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotPotato : MonoBehaviour
{
    [SerializeField]
    bool hasBomb;
    [SerializeField]
    bool canTakeBomb = true;
    bool hasLeftEnemy;
    GameObject bombImage;
    void Start()
    {
        bombImage = transform.GetChild(1).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBomb)
        {
            bombImage.SetActive(true);
            canTakeBomb = false;
        }
        if(!hasBomb)
        {
            bombImage.SetActive(false);
            canTakeBomb = true;
        }
     
    }
    public void SetHasBomb(bool _bool)
    {
        hasBomb = _bool;
    }
    public void SetCanTakeBomb(bool _bool)
    {
        canTakeBomb = _bool;
    }
    public bool HasBomb()
    {
        return hasBomb;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!hasLeftEnemy)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(ResetCanTakeBomb());
                hasLeftEnemy = true;
            }
            if (other.gameObject.tag == "Player2")
            {
                StartCoroutine(ResetCanTakeBomb());
                hasLeftEnemy = true;
            }
            if (other.gameObject.tag == "Player3")
            {
                StartCoroutine(ResetCanTakeBomb());
                hasLeftEnemy = true;
            }
            if (other.gameObject.tag == "Player4")
            {
                StartCoroutine(ResetCanTakeBomb());
                hasLeftEnemy = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (hasBomb && other.GetComponent<PlayerHotPotato>().canTakeBomb)
            {
                other.GetComponent<PlayerHotPotato>().SetHasBomb(true);
                hasBomb = false;
            }
            if (other.GetComponent<PlayerHotPotato>().hasBomb && canTakeBomb)
            {
                other.GetComponent<PlayerHotPotato>().SetHasBomb(false);
                StartCoroutine(ResetPlayerMovement());
                hasBomb = true;
            }
            Debug.Log("1");
        }
        if (other.gameObject.tag == "Player2")
        {
            if (hasBomb && other.GetComponent<PlayerHotPotato>().canTakeBomb)
            {
                other.GetComponent<PlayerHotPotato>().SetHasBomb(true);
                hasBomb = false;
            }
            if (other.GetComponent<PlayerHotPotato>().hasBomb && canTakeBomb)
            {
                other.GetComponent<PlayerHotPotato>().SetHasBomb(false);
                StartCoroutine(ResetPlayerMovement());
                hasBomb = true;
            }
            Debug.Log("2");
        }
        if (other.gameObject.tag == "Player3")
        {
            if (hasBomb && other.GetComponent<PlayerHotPotato>().canTakeBomb)
            {
                other.GetComponent<PlayerHotPotato>().SetHasBomb(true);
                hasBomb = false;
            }
            if (other.GetComponent<PlayerHotPotato>().hasBomb && canTakeBomb)
            {
                other.GetComponent<PlayerHotPotato>().SetHasBomb(false);
                StartCoroutine(ResetPlayerMovement());
                hasBomb = true;
            }
            Debug.Log("3");
        }
        if (other.gameObject.tag == "Player4")
        {
            if (hasBomb && other.GetComponent<PlayerHotPotato>().canTakeBomb)
            {
                other.GetComponent<PlayerHotPotato>().SetHasBomb(true);
                hasBomb = false;
            }
            if (other.GetComponent<PlayerHotPotato>().hasBomb && canTakeBomb)
            {
                other.GetComponent<PlayerHotPotato>().SetHasBomb(false);
                StartCoroutine(ResetPlayerMovement());
                hasBomb = true;
            }
            Debug.Log("4");
        }
    }
    IEnumerator ResetCanTakeBomb()
    {
        yield return new WaitForSeconds(1);
        canTakeBomb = true;
        hasLeftEnemy = false;
    }
    IEnumerator ResetPlayerMovement()
    {
        GetComponent<Rigidbody>().mass = 200;
        GetComponent<Rigidbody>().drag = 200;
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody>().mass = 1;
        GetComponent<Rigidbody>().drag = 0;
    }
}
