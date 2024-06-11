using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnimator;

    public float moveSpeed = 1.0f;
    public float jumpSpeed = 1.0f, jumpFrequency = 1.0f, nextJumpTime;

    bool facingRight = true;

    public bool isGrounded = false;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;

   
    public float rollSpeed = 5.0f;
    private bool isRolling = false;

    void Awake()
    {
    }

    
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        OnGroundCheck();

        HorizontalMove();

        if (playerRB.velocity.x < 0 && facingRight)
        {
            FlipFace();
        }
        else if (playerRB.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }

        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad))
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
        }
    }

    void FixedUpdate()
    {
    }

    void HorizontalMove()
    {
        if (!isRolling)
        {
            playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
            playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
        }
    }

    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }

    void Jump()
    {
        playerRB.AddForce(new Vector2(0f, jumpSpeed));
    }

    void Roll()
    {
        if (isGrounded && !isRolling)
        {
            isRolling = true;
            playerAnimator.SetBool("isRolling", true);
            float rollDirection = facingRight ? 1 : -1;
            playerRB.velocity = new Vector2(rollDirection * rollSpeed, playerRB.velocity.y);
            Invoke("EndRoll", 0.5f);
        }
    }

    void EndRoll()
    {
        isRolling = false;
        playerAnimator.SetBool("isRolling", false);
    }

    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer);
        playerAnimator.SetBool("isGroundedAnim", isGrounded);
    }
}