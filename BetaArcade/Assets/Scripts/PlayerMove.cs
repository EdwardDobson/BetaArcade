﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    public int ID;
    AudioSource Jump;
    AudioSource Walk;
    private float originalSpeed = 60f;
    private float speed = 60f;
    private float maxSpeed = 5f;
    private float jumpSpeed = 300.0f;
    private float rotationSpeed = 25f;
    private float dashSpeed = 8.0f;
    private float distanceToGround;
    private Vector3 movement;
    private Rigidbody rb;
    private bool isFrozen = false;
    private bool isGrounded;
    private Animator m_CharacterAnimator;

    public bool hasDashed;
    public bool hasPushed = false;
    [SerializeField]
    float shoveForce = 0;
    [SerializeField]
    float shoveRadius = 0;
    int bigJumps = 0;
    int powerUpCount = 0;
    [SerializeField]
    public float dashTimer = 0.5f;
    [SerializeField]
    public float shoveTimer = 0.5f;
    [SerializeField]
    bool jumpEnabled = true; //used in bomberman to disable the jump function
    [SerializeField]
    bool rotationEnabled = true;
    public Slider dashSlider;
    public Slider shoveSlider;
    public bool canMove = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("LateStart", 0.001f);
        distanceToGround = GetComponent<Collider>().bounds.extents.y/4;
        Walk = GetComponent<AudioSource>();
        if (jumpEnabled)
        {
            Jump = transform.Find("JumpAudioSource").GetComponent<AudioSource>();
        }
        m_CharacterAnimator = GetComponentInChildren<Animator>();
        MassPowerUpReset();

    }
    void LateStart()
    {
        var playerPicture = GameObject.Find("PlayerPicture" + ID);
        if (playerPicture != null)
        {
            shoveSlider = playerPicture.transform.GetChild(0).GetComponent<Slider>();
            dashSlider = playerPicture.transform.GetChild(7).GetComponent<Slider>();
            shoveSlider.value = 0.5f;
            dashSlider.value = 0.5f;
        }
        else
        {
            Debug.LogWarning("Cannot get PlayerPicture UI");
        }
    }
    private void Update()
    {

        if (canMove)
        {
            if (isGrounded && !isFrozen && jumpEnabled)
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

                                if (t.name == "PUJump" + "(Clone)")
                                {
                                    t.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
                                    t.gameObject.name = "";
                                    powerUpCount--;
                                }
                            }
                        }
                    }

                    Jump.Play();
                    if (m_CharacterAnimator != null)
                    {
                        // TODO if we have time we should implement this
                        m_CharacterAnimator.SetTrigger("Jump");
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
                if (rb.velocity.sqrMagnitude > 2f && !Walk.isPlaying)
                {
                    Walk.Play();
                }
            }
            if (!isGrounded && rb.velocity.y <= 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - (2 * Time.deltaTime), transform.position.z);
            }

            if (m_CharacterAnimator != null)
            {
                float horizontalMoveSpeed = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);
                m_CharacterAnimator.SetFloat("MoveSpeed", horizontalMoveSpeed);
                m_CharacterAnimator.SetFloat("RunMultiplier", 0.1f + (horizontalMoveSpeed / 6.5f));
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
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
                {
                    float y = rb.velocity.y;
                    rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
                    rb.velocity = new Vector3(rb.velocity.x, y, rb.velocity.z);
                }


                Vector3 lookDir = new Vector3(Input.GetAxis("Mouse X" + ID), 0, -Input.GetAxis("Mouse Y" + ID));
                if (Input.GetButton("Shove" + ID) && !hasPushed)
                {
                    hasPushed = true;
                    Debug.Log("Shoved");
                    Push();
                    StartCoroutine(ResetShove());
                }
                if (lookDir.magnitude > 0.5 && rotationEnabled == true)
                {
                    Quaternion lookRot = Quaternion.LookRotation(lookDir, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
                }
            }
            isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);
        }
    }
   public IEnumerator ResetDash()
    {

        yield return new WaitForSeconds(0.5f);
        hasDashed = false;
        dashTimer = 0.5f;
        dashSlider.value = dashTimer;
    }
    public IEnumerator ResetShove()
    {

        yield return new WaitForSeconds(0.5f);
        hasPushed = false;
        shoveTimer = 0.5f;
        shoveSlider.value = shoveTimer;
    }
    public void SetSpeed(int _speed)
    {
        maxSpeed = _speed;
    }
    void Push()
    {

        Vector3 pushPos = transform.GetChild(0).position;
        Collider[] colliders = Physics.OverlapBox(pushPos, transform.localScale);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(shoveForce, pushPos, shoveRadius, 0.8f);
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
                if (t.name == "PUSpeedup" + "(Clone)")
                {
                    t.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
                    t.gameObject.name = "";
                    powerUpCount--;
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
    public void IncreasePowerUpCount()
    {
        powerUpCount++;
    }
    public void DecreasePowerUpCount()
    {
        powerUpCount--;
    }
    public int GetPowerUpCount()
    {
        return powerUpCount;
    }
    public void MassPowerUpReset()
    {
        GameObject Clone = GameObject.Find("PlayerPicture" + ID);
        if (Clone != null)
        {
            foreach (Transform t in Clone.transform.transform)
            {
                if (t.gameObject.tag == "PowerUpSlot")
                {
                    t.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
                    t.gameObject.name = "";
                    t.GetComponent<Image>().sprite = null;
                }
            }
            powerUpCount = 0;
            bigJumps = 0;
            speed = originalSpeed;
        }
    }
}
