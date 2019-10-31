using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int ID;

    private float originalSpeed = 15f;
    private float speed = 15f;
    private float jumpSpeed = 180.0f;
    private float rotationSpeed = 12.5f;
    private float dashSpeed = 8.0f;
    private Vector3 movement;
    private Rigidbody rb;
    private bool isGrounded;
    private bool hasDashed;
    private bool hasPushed = false;
    [SerializeField]
    float shoveForce = 0;
    [SerializeField]
    float shoveRadius = 0;
    int bigJumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump" + ID))
            {
                if (bigJumps > 0)
                {
                    rb.AddForce(Vector3.up * jumpSpeed * 2f);
                    bigJumps--;
                }
                else
                    rb.AddForce(Vector3.up * jumpSpeed);

            }
            if (Input.GetButtonDown("Dash" + ID) && !hasDashed)
            {
                rb.AddForce(movement * dashSpeed, ForceMode.Impulse);
                hasDashed = true;
                StartCoroutine(ResetDash());
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal" + ID);
        float moveVertical = Input.GetAxisRaw("Vertical" + ID);
        //movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * speed;
        //movement.y = rb.velocity.y;
        //rb.velocity = movement;
        movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);

        Vector3 lookDir = new Vector3(Input.GetAxis("Mouse X" + ID), 0, -Input.GetAxis("Mouse Y" + ID));
        if (Input.GetButton("Shove" + ID) && !hasPushed)
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
        Collider[] colliders = Physics.OverlapBox(pushPos, transform.localScale / 4);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
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

    public void IncreaseMovementSpeed()
    {
        speed = (speed * 1.5f);
        StartCoroutine(SpeedReset(5));
    }
    IEnumerator SpeedReset(float time)
    {
        yield return new WaitForSeconds(time);
        speed = originalSpeed;
    }

    public void AddBigJumps(int count)
    {
        bigJumps += count;
    }
}
