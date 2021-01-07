using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAllinOne : MonoBehaviour
{
    [TagSelector]
    public string playerSpawnTag;

    //Health Variables
    public int health;
    public int maxHealth;

    //MovementVariables
    public float speed;
    private float moveInput;
    public float defaultSpeed;
    public float dashSpeed;
    public bool canDash;

    //Jump Variables
    public float jumpForce;
    public float defaultJumpForce;
    public float dashJumpBoost;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //Cooldowns
    public float dashCooldown;
    public float dashMomeDropoff;
    public float dashDropoffCalc;

    //Objects
    private Rigidbody2D rb;
    public GameObject deathEffect;
    public Text healthText;
    private PlayerSpawn playerSpawn;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        playerSpawn = GameObject.FindGameObjectWithTag(playerSpawnTag).GetComponent<PlayerSpawn>();
        healthText.text = health + "/" + maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health != int.Parse(healthText.text.ToString().Split('/')[0]))
        {
            UpdateText();
        }
        if (health <= 0)
        {
            ObjectPool.Spawn(deathEffect, transform.position, Quaternion.identity);
            playerSpawn.RespawnPlayer();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetBool("isRunning", true);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            anim.SetTrigger("Jump");
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
    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateText();
    }
    public void UpdateText()
    {
        healthText.text = health + "/" + maxHealth;
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
