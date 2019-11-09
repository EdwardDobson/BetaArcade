﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class HideText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "EndZone")
        {

            GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
