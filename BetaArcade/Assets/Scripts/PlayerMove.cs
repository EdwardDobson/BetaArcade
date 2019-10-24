using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int ID;

    private float speed = 20.0f;
    private float jumpSpeed = 80.0f;
    private float rotationSpeed = 12.5f;
    private float dashSpeed = 8.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 movement;
    private Rigidbody rb;
    private bool isGrounded;
    private bool hasDashed;
    private bool hasPushed = false;
    [SerializeField]
    float shoveForce = 0;
    [SerializeField]
    float shoveRadius = 0;

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
            if (Input.GetButton("Jump" + ID))
            {
                rb.AddForce(Vector3.up * jumpSpeed);

            }
            if (Input.GetButton("Dash" + ID) && !hasDashed)
            {
                rb.AddForce(movement * dashSpeed, ForceMode.Impulse);
                hasDashed = true;
                StartCoroutine(ResetDash());
            }
        }
        float moveHorizontal = Input.GetAxis("Horizontal" + ID);
        float moveVertical = Input.GetAxis("Vertical" + ID);
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);

        Vector3 lookDir = new Vector3(Input.GetAxis("Mouse X" + ID), 0, -Input.GetAxis("Mouse Y" + ID));
        if(Input.GetButton("Shove" +ID) && !hasPushed)
        {
            hasPushed = true;
            Debug.Log("Shoved");
            Push();
            StartCoroutine(ResetShove());
        }
        if (lookDir.magnitude > 0.5)
        {
            Quaternion lookRot = Quaternion.LookRotation(lookDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);
        }
    }
    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(0.5f);
        hasDashed = false;
    }
    IEnumerator ResetShove()
    {
        yield return new WaitForSeconds(0.5f);
        hasPushed = false;
    }
    void Push()
    {
        Vector3 pushPos = transform.GetChild(1).position;
        Collider[] colliders = Physics.OverlapBox(pushPos, transform.localScale/4);
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(shoveForce, pushPos, shoveRadius, 3.0f);
            }
        }
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
