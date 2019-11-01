using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectTutorialStart : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject startButton;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem.SetSelectedGameObject(startButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
