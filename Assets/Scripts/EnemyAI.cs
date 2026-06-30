using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movimentação")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Detecção")]
    public float detectionRange = 6f;

    [Header("Patrulha")]
    public Transform pointA;
    public Transform pointB;

    [Header("Ataque")]
    public float attackRange = 1.1f;
    public int attackDamage = 1;
    public float attackCooldown = 1f;

    private Transform targetPoint;
    private Transform player;

    private Rigidbody2D rb;
    private float nextAttack;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool chasing;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        targetPoint = pointB;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Attack();
        }
        else if (distance <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {        
        animator.SetBool("IsWalking", true);

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }

        Flip(targetPoint.position.x - transform.position.x);
    }

    void ChasePlayer()
    {
        animator.SetBool("IsWalking", true);

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime);

        Flip(player.position.x - transform.position.x);
    }

    void Flip(float direction)
    {
        if (direction > 0)
            spriteRenderer.flipX = false;
        else if (direction < 0)
            spriteRenderer.flipX = true;
    }

    void Attack()
    {
        rb.linearVelocity = Vector2.zero;

        animator.SetBool("IsWalking", false);

        if (Time.time < nextAttack)
            return;

        nextAttack = Time.time + attackCooldown;

        animator.SetTrigger("IsAttacking");

        PlayerHealth health =
            player.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(attackDamage);
        }
    }
}