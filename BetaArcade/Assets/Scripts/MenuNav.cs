using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNav : MonoBehaviour
{

    StandaloneInputModule[] inputs;
    // Start is called before the first frame update
    void Start()
    {

        inputs = GetComponents<StandaloneInputModule>();
       // standaloneInputModule2 = transform.GetChild(0).GetComponent<StandaloneInputModule>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetAxisRaw("Vertical1") > 0 || Input.GetAxisRaw("Vertical1") < 0)
        {
            inputs[0].enabled = false;
            inputs[1].enabled = true;

        }
        if (Input.GetAxisRaw("DpadV") > 0 || Input.GetAxisRaw("DpadV") < 0)
        {
            inputs[1].enabled = false;
            inputs[0].enabled = true;
        }
   
        if(inputs[0].enabled)
        {
            inputs[1].enabled = false;
            inputs[0].enabled = true;
        }
        if(inputs[1].enabled)
        {
            inputs[0].enabled = false;
            inputs[1].enabled = true;
        }

    }
}
