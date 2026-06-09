using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 8f;

    private Collider2D playerCollider;
    private Collider2D currentPlatform;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Jump();
        DropThrough();
    }

    void Move()
    {
        float move = 0f;

        if (Keyboard.current.aKey.isPressed)
        {
            move = -1f;

            if (Keyboard.current.shiftKey.isPressed)
                move = -1.5f;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            move = 1f;

            if (Keyboard.current.shiftKey.isPressed)
                move = 1.5f;
        }

        rb.linearVelocity =
            new Vector2(move * speed, rb.linearVelocity.y);

        if (move != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (move < 0)
        {
        spriteRenderer.flipX = true;
        }
        else if (move > 0)
        {
        spriteRenderer.flipX = false;
        }

    }

    void Jump()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity =
                new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void DropThrough()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame &&
            currentPlatform != null)
        {
            StartCoroutine(DropThroughPlatform());
        }
    }

    IEnumerator DropThroughPlatform()
    {
        Physics2D.IgnoreCollision(
            playerCollider,
            currentPlatform,
            true);
    rb.linearVelocity =
        new Vector2(rb.linearVelocity.x, -2f);
        yield return new WaitForSeconds(0.5f);

        Physics2D.IgnoreCollision(
            playerCollider,
            currentPlatform,
            false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = collision.collider;
        }
    }
}