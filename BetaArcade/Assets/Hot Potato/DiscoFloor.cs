using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoFloor : MonoBehaviour
{
    public Material[] materials;
    public float changeInterval;
    public Renderer rend;
    public float Timer;
    public int textureIndex;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
 
        if(Timer <=0)
        {
            Timer = changeInterval;
            textureIndex++;
            textureIndex %= materials.Length;
            rend.material = materials[textureIndex];
        }
        
    }
}
