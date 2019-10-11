using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotPotato : MonoBehaviour
{
    [SerializeField]
    bool hasBomb;
    GameObject bombImage;
    void Start()
    {
        bombImage = transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBomb)
        {
            bombImage.SetActive(true);
        }
        else bombImage.SetActive(false);
    }
    public void SetHasBomb(bool _bool)
    {
        hasBomb = _bool;
    }
    public bool HasBomb()
    {
        return hasBomb;
    }
}
