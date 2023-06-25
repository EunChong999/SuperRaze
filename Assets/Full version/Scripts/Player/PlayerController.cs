using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Move
    [HideInInspector] public float horizontal;
    public float movingSpeed = 7f;
    private float movingSpeedTemp;

    // Jump
    public float jumpingPower = 7f;
    private bool doubleJump;

    // Flip
    [HideInInspector] public bool isFacingRight = true;

    // WallSlide
    private bool isWallSliding;
    public float wallSlidingSpeed = 2f;

    // WallJump
    private bool isWallJumping;
    private float wallJumpingDirection;
    public float wallJumpingPower = 7f;
    private float wallJumpingTime = 0.2f;
    [HideInInspector] public float wallJumpingCounter;
    [HideInInspector] public Vector2 wallJumpingVector = new Vector2(1f, 2f);

    // Common
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] private Transform body;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask objectLayer;
    //[SerializeField] private PlayerCollisionChecker collisionChecker;

    private void Start()
    {
        movingSpeedTemp = movingSpeed;
        wallJumpingVector *= wallJumpingPower;
    }

    private void Update()
    {
        Move();
        Jump();

        if (!isWallJumping)
        {
            Flip();
        }

        WallSlide();
        WallJump();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.25f, objectLayer);
    }

    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.25f, objectLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.25f);
        Gizmos.DrawWireSphere(wallCheck.position, 0.25f);
    }

    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * movingSpeed, rb.velocity.y);
        }

        if (horizontal != 0)
        {
            anim.SetBool("isRuning", true);
        }
        else 
        {
            anim.SetBool("isRuning", false);
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Shoot") && IsGrounded())
        {
            movingSpeed = 1f;
        }
        else
        {
            movingSpeed = movingSpeedTemp;
        }
    }

    private void Jump()
    {
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump") && (rb.velocity.y) > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = body.localScale;
            localScale.x *= -1f;
            body.localScale = localScale;
        }
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            movingSpeed = 0f;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else if(IsWalled() && IsGrounded())
        {
            if (Input.GetButtonDown("Jump") && horizontal != 0f) 
            {
                isWallSliding = true;
            }
            else
            {
                isWallSliding = false;
                movingSpeed = movingSpeedTemp;
            }
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding) 
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;

            if (isFacingRight)
            {
                if (wallJumpingVector.x < 0)
                {
                    wallJumpingVector.x *= -1;
                }
            }
            else
            {
                if (wallJumpingVector.x > 0)
                {
                    wallJumpingVector.x *= -1;
                }
            }

            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingVector.x, wallJumpingVector.y);

            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = body.localScale;
                localScale.x *= -1f;
                body.localScale = localScale;
            }
        }
        else if ((IsGrounded() || Input.GetButtonUp("Jump")) && rb.velocity.y < 14f)
        {
            StopWallJumping();
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }
}

