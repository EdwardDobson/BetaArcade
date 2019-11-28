using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    public int ID;

    private float originalSpeed = 15f;
    private float speed = 60f;
    private float maxSpeed = 5f;
    private float jumpSpeed = 600.0f;
    private float rotationSpeed = 25f;
    private float dashSpeed = 8.0f;
    private float distanceToGround;
    private Vector3 movement;
    private Rigidbody rb;
    private bool isFrozen = false;
    private bool isGrounded;
    private bool hasDashed;
    private bool hasPushed = false;
    [SerializeField]
    float shoveForce = 0;
    [SerializeField]
    float shoveRadius = 0;
    int bigJumps = 0;
    int powerUpCount = 0;
    [SerializeField]
    float dashTimer = 0.5f;
    [SerializeField]
    float shoveTimer = 0.5f;
    Slider dashSlider;
    Slider shoveSlider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("LateStart", 0.1f);
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }
    void LateStart()
    {
        shoveSlider = GameObject.Find("PlayerPicture" + ID).transform.GetChild(0).GetComponent<Slider>();
        dashSlider = GameObject.Find("PlayerPicture" + ID).transform.GetChild(7).GetComponent<Slider>();
    }
    private void Update()
    {

        if (isGrounded && !isFrozen)
        {
            if (Input.GetButtonDown("Jump" + ID))
            {
                if (bigJumps > 0)
                {
                    rb.AddForce(Vector3.up * jumpSpeed * 2f);
                    bigJumps--;
                }
                else if (bigJumps <= 0)
                {

                    rb.AddForce(Vector3.up * jumpSpeed);
                    GameObject Clone = GameObject.Find("PlayerPicture" + ID);
                    if (Clone != null)
                    {
                        foreach (Transform t in Clone.transform)
                        {
                            if (t.name == "PUJump")
                            {
                                t.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
                            }
                        }
                    }
                }
            }
            if (Input.GetButtonDown("Dash" + ID) && !hasDashed)
            {
                rb.AddForce(movement * dashSpeed, ForceMode.Impulse);
                hasDashed = true;
                StartCoroutine(ResetDash());
            }
            if (hasDashed)
            {
                dashTimer -= Time.deltaTime;
                dashSlider.value = dashTimer;

            }
            if (hasPushed)
            {
                shoveTimer -= Time.deltaTime;
                shoveSlider.value = shoveTimer;
            }


        }
    }

  // Update is called once per frame
  void FixedUpdate()
    {
    if (!isFrozen)
      {
      float moveHorizontal = Input.GetAxisRaw("Horizontal" + ID);
      float moveVertical = Input.GetAxisRaw("Vertical" + ID);
      movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
      rb.AddForce(new Vector3(moveHorizontal * speed, 0, moveVertical * speed));
      //if(rb.velocity.sqrMagnitude < maxSpeed)
      //rb.AddForce(Time.deltaTime * movement.x * speed, 0, Time.deltaTime * movement.z * speed, ForceMode.VelocityChange);
      if (Mathf.Abs(rb.velocity.z) > maxSpeed || Mathf.Abs(rb.velocity.x) > maxSpeed)
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

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
    isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }
        IEnumerator ResetDash()
        {

            yield return new WaitForSeconds(0.5f);
            hasDashed = false;
            dashTimer = 0.5f;
            dashSlider.value = dashTimer;
        }
        IEnumerator ResetShove()
        {

            yield return new WaitForSeconds(0.5f);
            hasPushed = false;
            shoveTimer = 0.5f;
            shoveSlider.value = shoveTimer;
        }
        void Push()
        {

            Vector3 pushPos = transform.GetChild(0).GetChild(0).position;
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

        public void IncreaseMovementSpeed()
        {
            speed = (speed * 1.5f);
            StartCoroutine(SpeedReset(5));
        }
        IEnumerator SpeedReset(float time)
        {
            yield return new WaitForSeconds(time);
            GameObject Clone = GameObject.Find("PlayerPicture" + ID);
            if (Clone != null)
            {
                foreach (Transform t in Clone.transform.transform)
                {
                    if (t.name == "PUSpeedup")
                    {
                        t.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
                    }

                }
            }
            speed = originalSpeed;
        }

  public void ToggleFreeze(bool frozen)
    {
    isFrozen = frozen;
    }

        public void AddBigJumps(int count)
        {
            bigJumps += count;
        }
        public void IncreasePowerUpCount(int _count)
        {
            powerUpCount++;
        }
        public void DecreasePowerUpCount(int _count)
        {
            powerUpCount--;
        }
        public int GetPowerUpCount()
        {
            return powerUpCount;
        }
    }
