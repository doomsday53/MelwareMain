using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;
    public float defaultSpeed;
    public float defaultJumpForce;
    public float dashSpeed;
    public float dashJumpBoost;
    public float dashCooldown;
    public float dashMomeDropoff;

    public bool isGrounded;
    public bool canDash;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    public float dashDropoffCalc;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;

        }
        
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            useDashAbility();
        }

    }
    void useDashAbility()
    {
        StartCoroutine(DashAbility());

    }
    IEnumerator DashAbility()
    {
        canDash = false;
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isDashing", true);
        speed = dashSpeed;
        jumpForce = dashJumpBoost;
        yield return new WaitForSeconds(dashCooldown);
        while (speed > defaultSpeed)
        {
            speed -= dashMomeDropoff;
            yield return null;
        }
        if (speed < defaultSpeed)
        {
            speed = defaultSpeed;
        }
        jumpForce = defaultJumpForce;
        canDash = true;
        anim.SetBool("isDashing", false);
    }

}
