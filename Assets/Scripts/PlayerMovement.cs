using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    float movementSpeed = 2;
    [SerializeField]
    float jumpStrength = 5;
    [SerializeField]
    Transform groundDetectCenterPoint;
    [SerializeField]
    float groundDetectRadius = 0.2f;
    [SerializeField]
    private Slider gravitationalSlider;

    [SerializeField]
    LayerMask whatCountsAsGround;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource errorAudioSource;

    [SerializeField]
    private float gravityChargeDecreaseRate = 0.1f;
    [SerializeField]
    private float chargeRechargeRate = 0.002f;
    private float chargeMax = 100;
    private float currentChargeLevel;
    private bool isRecharging;
    private bool canFlip = false;

    private float horizontalInput;

    private bool isOnGround;
    private bool isOnCeiling;

    private Vector2 gravity;

    private bool shouldJump;
    private bool facingRight = true;
    public bool doubleJump = false;
    Animator anim;

    [SerializeField]
    Animator sliderAnim;

    public bool gravitySwitch;

    private Vector2 jumpForce;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    Rigidbody2D myRigidbody;

    
    //Use this for initialization
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gravity = Physics.gravity;
        anim = GetComponent<Animator>();
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        sliderAnim = GetComponent<Animator>();
        sliderAnim.SetBool("canFlip", true);
        
        myRigidbody = GetComponent<Rigidbody2D>();
        jumpForce = new Vector2(0, jumpStrength);

    }

    private void Update()
    {
        GetMovementInput();
        GetJumpInput();
        UpdateIsOnGround();
        GravityReverse();
        CheckForDepletedCharge();
        PlayerTurnsRedWhenOutofGravity();
        if ((isOnGround) && Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Ground", true);
            myRigidbody.AddForce(new Vector2( 0, jumpStrength));
            audioSource.Play();
        }
        if (currentChargeLevel > 0 && !isRecharging)
        {
            canFlip = true;
            if (Input.GetButtonDown("Jump") || canFlip) //Detect if player presses space to flip gravity
            {
                //StartCoroutine(GravityChargeConsumptionCoroutine());
                gravitySwitch = !gravitySwitch;
                if (gravitySwitch)
                {
                    StartCoroutine(GravityChargeConsumptionCoroutine());
                    Physics.gravity = new Vector2(0, 20); //Invert
                }
                else if (!gravitySwitch)
                {
                    StartCoroutine(GravityChargeConsumptionCoroutine());
                    Physics.gravity = new Vector2(0, -20); //Default unity
                }
                audioSource.Play();
            }
        }
        UpdateGUICharge();

    }

    private void CheckForDepletedCharge()
    {
        if(currentChargeLevel <= 0)
        {
            //StartCoroutine(ChargeDepleteRechargeCoroutine());
        }
    }

    private void UpdateGUICharge()
    {
        gravitationalSlider.value = currentChargeLevel;
    }

    private IEnumerator GravityChargeConsumptionCoroutine()
    {
        while (Input.GetButtonDown("Jump") && currentChargeLevel > 0 && !isRecharging)
        {
            currentChargeLevel-=7;
            yield return new WaitForSeconds(gravityChargeDecreaseRate);
        }
    }

    private IEnumerator ChargeDepleteRechargeCoroutine()
    {
        isRecharging = true;
        while(currentChargeLevel < chargeMax)
        {
            shouldJump = false;
            currentChargeLevel++;
            yield return new WaitForSeconds(chargeRechargeRate);
        }
        isRecharging = false;
        shouldJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            currentChargeLevel = chargeMax;
        }
    }

    private void PlayerTurnsRedWhenOutofGravity()
    {
        if(currentChargeLevel == 0)
        {
            spriteRenderer.color = Color.red;
        }
        else if(currentChargeLevel > 0)
        {
            spriteRenderer.color = Color.white;
        }
    }

    private IEnumerator SliderTurnsRedIfCannotFlip()
    {
        if(!canFlip)
        {
            sliderAnim.SetBool("canFlip", false);
            yield return new WaitForSeconds(2f);
            //sliderAnim.SetBool("canFlip", true);
        }
    }

    private void GravityReverse()
	{
        if (canFlip)
        {
            if (Input.GetButtonDown("Jump"))
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.6f);
                myRigidbody.gravityScale *= -1;
                jumpForce = jumpForce * -1;

                myRigidbody.AddForce(jumpForce, ForceMode2D.Impulse);

                //facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.y *= -1;
                transform.localScale = theScale;
                StartCoroutine(GravityChargeConsumptionCoroutine());
                audioSource.Play();

            }
        }
        else
        {
            if(Input.GetButtonDown("Jump"))
            {
                //sliderAnim.SetBool("canFlip", true);
                StartCoroutine(SliderTurnsRedIfCannotFlip());
                errorAudioSource.Play();
                sliderAnim.SetBool("canFlip", true);
            }    
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    private void GetJumpInput()
    {
        if (Input.GetButtonDown("Fire2") && isOnGround || isOnCeiling)
        {
            shouldJump = true;
        }
    }

    private void GetMovementInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

    private void FixedUpdate()
    {
        Physics.gravity = gravity;
        isOnGround = Physics2D.OverlapCircle(groundDetectCenterPoint.position, groundDetectRadius, whatCountsAsGround);
        anim.SetBool("Ground", isOnGround);
		//anim.SetBool ("Ceiling", isOnCeiling);
        Debug.Log("Are we on the ground? " + isOnGround);
        anim.SetFloat("vSpeed", myRigidbody.velocity.y);
        Move();
        Jump();
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void UpdateIsOnGround()
    {
        Collider2D[] groundObjects = Physics2D.OverlapCircleAll(groundDetectCenterPoint.position, groundDetectRadius, whatCountsAsGround);
        isOnGround = groundObjects.Length > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
    }

    private void Jump()
    {
        if (shouldJump)
        {
            anim.SetBool("Ground", true);
            anim.SetFloat("vSpeed", myRigidbody.velocity.y);
            myRigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
            //myRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            isOnGround = false;
			isOnCeiling = false;
            shouldJump = false;
            audioSource.Play();
           
        }
    }

    private void Move()
    {
        myRigidbody.velocity = new Vector2(horizontalInput * movementSpeed, myRigidbody.velocity.y);
    }


}
