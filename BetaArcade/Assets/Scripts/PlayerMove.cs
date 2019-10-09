using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController characterController;
    private float speed = 5.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;
    private float rotateSpeed = 75.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotateDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            rotateDirection = new Vector3(0, Input.GetAxis("Mouse X"), 0);
            rotateDirection *= rotateSpeed;
            

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
            /*if (Input.GetButton("Dash"))
            {
                moveDirection.y = jumpSpeed;
            }*/
        }
        transform.Rotate(rotateDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
