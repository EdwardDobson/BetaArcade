using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private int WeaponSelection = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int PreviousSelectedWeapon = WeaponSelection;
        if (Input.GetButtonDown("RB"))
        {
            if (WeaponSelection >= transform.childCount - 1)
            {
                WeaponSelection = 0;
                Debug.Log("i WORK 222222");
            }
            else
            {
                WeaponSelection++;
            }
        }

        if (Input.GetButton("RB"))
        {
            if (WeaponSelection <= 0)
            {
                WeaponSelection = transform.childCount - 1;
                Debug.Log("i WORK 3333");
            }
            else
            {
                WeaponSelection--;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponSelection = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            WeaponSelection = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            WeaponSelection = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            WeaponSelection = 3;
        }
        
        if (PreviousSelectedWeapon != WeaponSelection)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == WeaponSelection)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
