using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialstart : MonoBehaviour
{

    public GameObject tutorial;
    public void ClickStart()
    {
        tutorial.SetActive(false);
    }

}
