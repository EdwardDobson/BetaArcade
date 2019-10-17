using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 20.0f;
    private float jumpSpeed = 80.0f;
    private float rotateSpeed = 75.0f;
    private float rotationSpeed = 75.0f;
    private float dashSpeed = 8.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotateDirection = Vector3.zero;
    private Vector3 movement;
    private Rigidbody rb;
    private bool isGrounded;
    private bool hasDashed;

    public string HorizontalPlayer = "Horizontal_P1";
    public string VerticalPlayer = "Vertical_P1";
    public string Jump = "Jump_P1";
    public string Dash = "Dash_P1";
    public string MouseX = "Mouse X_P1";


    // Start is called before the first frame update
    void Start()
    {
    rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrounded)
        {
            if (Input.GetButton("Jump_P1"))
            {
                if (Input.GetButton(Jump))
                {
                    rb.AddForce(Vector3.up * jumpSpeed);

                }
                if (Input.GetButton(Dash) && !hasDashed)
                {
                    rb.AddForce(movement * dashSpeed, ForceMode.Impulse);
                    hasDashed = true;
                    StartCoroutine(ResetDash());
                }
            }
            float moveHorizontal = Input.GetAxis(HorizontalPlayer);
            Debug.Log(moveHorizontal);
            float moveVertical = Input.GetAxis(VerticalPlayer);
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
            rotateDirection = new Vector3(0, Input.GetAxis(MouseX), 0);
            rotateDirection *= rotateSpeed;
            transform.Rotate(rotateDirection * Time.deltaTime);

            Vector3 lookDir = new Vector3(Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y"));

            if (lookDir.magnitude > 0.5)
            {
                Quaternion lookRot = Quaternion.LookRotation(lookDir, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);
            }
        }
    }
  IEnumerator ResetDash()
    {
    yield return new WaitForSeconds(0.5f);
    hasDashed = false;
    }
  private void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.tag == "Ground")
      {
      isGrounded = true;
      }
    }
  private void OnCollisionExit(Collision collision)
    {
    if (collision.gameObject.tag == "Ground")
      {
      isGrounded = false;
      }
    }
  }
