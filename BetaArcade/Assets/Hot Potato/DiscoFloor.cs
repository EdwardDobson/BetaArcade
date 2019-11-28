using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoFloor : MonoBehaviour
{
    public Texture[] textures;
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
            textureIndex %= textures.Length;
            rend.material.mainTexture = textures[textureIndex];
        }
        

        /*
         if (textures.Length == 0)
             return;
         Timer -= Time.deltaTime;
         if(Timer <=0)
         {
             Timer = changeInterval;

             textureIndex++;

             rend.material.SetTexture("_BaseMap", textures[textureIndex]);
             if(textureIndex >= textures.Length)
             {
                 textureIndex = 0;
             }
             Debug.Log("Texture changed");

         }

            if (textures.Length == 0)
            return;

            textureIndex = Mathf.FloorToInt(Time.time / changeInterval);
            textureIndex = textureIndex % textures.Length;
            rend.material.mainTexture = textures[textureIndex];



         */
        
       
    }
}
