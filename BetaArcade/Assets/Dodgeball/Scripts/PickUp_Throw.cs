using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Throw : MonoBehaviour
{
    public float speed = 20;
    public bool canHold = true;
    public GameObject Ball;
    public Transform guide;
    private bool ivepressedabutton = false;

    void Update()
    {

        if (Input.GetButtonDown("Fire1") && !ivepressedabutton)
        {
            Debug.Log("PressedButton");
            ivepressedabutton = true;
            if (!canHold)
                ThrowOrDrop();
            else
            {
                Pickup();
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ivepressedabutton = false;
        }
        //Ball.transform.position = guide.position; //Moves the ball if you can't hold the ball
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            if (!Ball) // if the player doesn't have anything
                Ball = col.gameObject;
            Debug.Log("Touched Ball");
        }
    }

    private void Pickup()
    {
        if (!Ball) //If we don't have a ball
            return;
        //We set the object parent to our guide empty object i.e become it's child
        Ball.transform.SetParent(guide);
        //Set gravity to false while holding it
        Ball.GetComponent<Rigidbody>().useGravity = false;
        //we apply the same rotation our main object (Camera) has.
        Ball.transform.localRotation = transform.rotation;
        //We re-position the ball on our guide object
        Ball.transform.position = guide.position;
        //Set the ball to be active
        Ball.GetComponent<Ball>().IsActive = true;
        canHold = false;
    }
    private void ThrowOrDrop()
    {
        if (!Ball)
            return;
        //Set our Gravity to true again.
        Ball.GetComponent<Rigidbody>().useGravity = true;
        // we don't have anything to do with our ball anymore
        Ball = null;
        //Apply velocity on throwing
        guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        //Unparent the ball
        guide.GetChild(0).parent = null;
        canHold = true;
    }

}
