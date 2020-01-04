using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeClear : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> AddButtons = new List<GameObject>();
    [SerializeField]
    List<GameObject> RemoveButtons = new List<GameObject>();
    void Start()
    {
        for(int i = 0; i < 9; ++i)
        {
            AddButtons.Add(transform.GetChild(4).GetChild(i).gameObject);
        }
        for (int i = 0; i < 9; ++i)
        {
            RemoveButtons.Add(transform.GetChild(3).GetChild(i).gameObject);
        }
    }
    public void ClearButtons()
    {
        for (int i = 0; i < 9; ++i)
        {
            AddButtons[i].SetActive(true);
        }
        for (int i = 0; i < 9; ++i)
        {
            RemoveButtons[i].SetActive(false);
        }
    }
}
