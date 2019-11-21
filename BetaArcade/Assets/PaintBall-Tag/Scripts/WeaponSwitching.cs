using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private int WeaponSelection = 0;
    private bool f = false;
    public int ID;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int PreviousSelectedWeapon = WeaponSelection;
        if (Input.GetButtonDown("RB" + ID))
        {
            if (WeaponSelection >= transform.childCount - 1)
            {
                WeaponSelection = 0;
                Debug.Log("RB1 Works Correctly");
            }
            else
            {
                WeaponSelection++;
            }
        }

        if (Input.GetButtonDown("LB" + ID))
        {        
            if (WeaponSelection <= 0)
                {
                    if (f == false)
                    {
                    Debug.Log("LB1 Works Correctly");
                    f = true;
                    StartCoroutine(Waiter());
                    WeaponSelection = transform.childCount - 1;
                    }
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

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        f = false;
    }
}

