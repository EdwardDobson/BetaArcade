using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Throw : MonoBehaviour
{
    public float speed = 20;
    public bool canHold = true;
    public bool PickedUpBall = false;
    public GameObject ChildBall;
    public Transform guide;
    private bool ivepressedabutton = false;

    private Vector3 ballScale;

    public int id;
    PlayerMove PM;

    private void Start()
    {
        PM = GetComponent<PlayerMove>();
        id = PM.ID;
    }

    void Update()
    {
        if (Input.GetButtonDown("Y" + id) && !ivepressedabutton)
        {
            ivepressedabutton = true;
            if (!canHold)
                ThrowOrDrop();
            else
            {
                Pickup();
            }
        }
        if (Input.GetButtonUp("Y" + id))
        {
            ivepressedabutton = false;
        }

        if (PickedUpBall == true)
        {
                    //We re-position the ball on our guide object
            ChildBall.transform.position = guide.position;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            if (!ChildBall) // if the player doesn't have anything
                ChildBall = col.gameObject;
        }
    }

    private void Pickup()
    {
        if (!ChildBall) //If we don't have a ball
            return;

        PickedUpBall = true;
        //We set the object parent to our guide empty object i.e become it's child
        ChildBall.transform.SetParent(guide);

        ChildBall.gameObject.tag = "B"+id;

        //Set gravity to false while holding it
        ChildBall.GetComponent<Rigidbody>().useGravity = false;
        ChildBall.GetComponent<Rigidbody>().detectCollisions = false;

        ChildBall.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);

        //we apply the same rotation our main object (Camera) has.
        ChildBall.transform.localRotation = transform.rotation;

        //Set the ball to be active
        canHold = false;
    }

    private void ThrowOrDrop()
    {
        if (!ChildBall)
            return;

        ChildBall.GetComponent<Ball>().BallThrown();

        //Set our Gravity to true again.
        ChildBall.GetComponent<Rigidbody>().useGravity = true;
        ChildBall.GetComponent<Rigidbody>().detectCollisions = true;


        // we don't have anything to do with our ball anymore
        ChildBall = null;
        //Apply velocity on throwing
        guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;

        //Unparent the ball
        guide.GetChild(0).parent = null;

        PickedUpBall = false;

        canHold = true;
    }

}
