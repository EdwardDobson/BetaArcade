using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoFloor : MonoBehaviour
{
    public Texture[] textures;
    public float changeInterval;
    public Renderer rend;
    public float Timer;
    int textureIndex;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textures.Length == 0)
            return;
        Timer -= Time.deltaTime;
        if(Timer <=0)
        {
            Timer = changeInterval;

            textureIndex++;

            rend.material.SetTexture(textures[1].name, textures[1]);
            if(textureIndex >= textures.Length)
            {
                textureIndex = 0;
            }
        }
      
    }
}
