using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ModColour(int _id)
    {
        mat.SetColor("_BaseColor", IDToColor(_id));
        mat.SetColor("_BaseColor",new Vector4(mat.GetColor("_BaseColor").r, mat.GetColor("_BaseColor").g, mat.GetColor("_BaseColor").b, mat.GetColor("_BaseColor").a - 0.5f));
    }

    private Color IDToColor(int id)
    {
        switch (id)
        {
            case 1:
                return Color.red;
            case 2:
                return Color.yellow;
            case 3:
                return Color.green;
            case 4:
                return Color.blue;
            case 5:
                return Color.white;
            default:
                Debug.LogError("Player has no ID");
                break;
        }
        return Color.clear;
    }
}
