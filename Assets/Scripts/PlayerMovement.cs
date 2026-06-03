using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 8f;

    [Header("Respawn Settings")]
    public Vector3 respawnPoint;
    

    private Collider2D playerCollider;
    private Collider2D currentPlatform;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool fallen = false;

    IEnumerator DropThroughPlatform()
    {
        Physics2D.IgnoreCollision(playerCollider, currentPlatform, true);
        
        yield return new WaitForSeconds(0.5f);

        Physics2D.IgnoreCollision(playerCollider, currentPlatform, false);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {        
        Move();
        Jump();
        DropThrough();
    if (fallen)
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                ReturnToSpawnPoint();
                fallen = false;
            }
        }
    }

    void Move()
    {
        float move = 0f;

        if (Keyboard.current.aKey.isPressed)
        {
            move = -1f;
            if(Keyboard.current.shiftKey.isPressed)
                move = -1.5f;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            move = 1f;
            if(Keyboard.current.shiftKey.isPressed)
                move = 1.5f;
        }
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
    }
    void Jump()
    {
        if(Keyboard.current.wKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    void DropThrough()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame && currentPlatform != null)
        {
            StartCoroutine(DropThroughPlatform());
        }
    }


    private void ReturnToSpawnPoint()
    {
        transform.position = new Vector3(respawnPoint.x, respawnPoint.y, transform.position.z);
        rb.linearVelocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FallZone"))
        {
            fallen = true;
            Debug.Log("Você caiu!");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    if (collision.gameObject.CompareTag("Platform")){
            currentPlatform = collision.collider;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}