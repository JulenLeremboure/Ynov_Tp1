using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Android;

public class Knight_movements : MonoBehaviour
{
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private Animator animator;

    public float speed;
    public float jumpForce = 200;
    public Transform groundCheckTransform;
    public LayerMask groundLayer;
    public Transform spawnPosition;

    private bool isLookingRightSide;
    private bool isJumping = false;
    private bool isGrounded;
    private bool canDoubleJump = false;
    private bool canJumpBeReseted;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Déplacement
        float move = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * move * speed * Time.deltaTime);

        //Test de la collision du personnage avec le sol
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.4f, groundLayer);

        animator.SetFloat("velocity", Math.Abs(move));
        animator.SetFloat("verticalVelocity", rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);

        if (!isJumping)
        {
            UpdateSpriteFlip(move);
        }

        //Double jump
        if (HasPlayerLeftPlatformWithoutJumping())//If the player left a platform without jumping
        {
            isJumping = true;
            canDoubleJump = true;
            StartCoroutine(CanJumpBeResetCooldownCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.Space) && (!isJumping || canDoubleJump))
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpForce));

            if (!canDoubleJump && isGrounded) //First jump
            {
                canDoubleJump = true;

                ResetYVelocity();
                StartCoroutine(CanJumpBeResetCooldownCoroutine());
            }
            else //Second jump
            {
                canDoubleJump = false;

                ResetYVelocity();
                UpdateSpriteFlip(move);
                animator.SetTrigger("canDoubleJump");
            }
        }

        if (isGrounded && canJumpBeReseted)
        {
            ResetJump();
        }
    }

    private void UpdateSpriteFlip(float currentMovingDirection)
    {
        if (currentMovingDirection < -0.1f && isLookingRightSide)
        {
            spriteRenderer.flipX = true;
            isLookingRightSide = false;
        }
        else if (currentMovingDirection > 0.1f && !isLookingRightSide)
        {
            spriteRenderer.flipX = false;
            isLookingRightSide = true;
        }
    }

 

    //To prevent isGrounded to reset too soon, when the player has pressed space bar but he is still "grounded" the next frame
    private IEnumerator CanJumpBeResetCooldownCoroutine() 
    {
        canJumpBeReseted = false;
        yield return new WaitForSeconds(0.2f);
        canJumpBeReseted = true;
    }

    private void ResetYVelocity()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.angularVelocity = 0;
    }

    private bool HasPlayerLeftPlatformWithoutJumping()
    {
        return !isJumping && !canDoubleJump && !isGrounded;
    }

    private void ResetJump()
    {
        canDoubleJump = false;
        isJumping = false;
        canJumpBeReseted = false;
    }
}
