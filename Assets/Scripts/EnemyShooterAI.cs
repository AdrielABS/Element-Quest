using UnityEngine;

public class EnemyAIShooter : MonoBehaviour
{
    [Header("Movimento")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Detecção")]
    public float detectionRange = 6f;
    public float shootRange = 8f;

    [Header("Patrulha")]
    public Transform pointA;
    public Transform pointB;

    [Header("Ataque")]
    public float attackRange = 1.2f;
    public int attackDamage = 1;
    public float attackCooldown = 1f;

    [Header("Tiro")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootCooldown = 2f;


    private Transform player;
    private Transform targetPoint;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float nextAttack;
    private float nextShoot;
    private bool facingLeft;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        targetPoint = pointB;
    }


    void Update()
    {
        float distance = Vector2.Distance(
            transform.position,
            player.position);


        if(distance <= attackRange)
        {
            Attack();
        }
        else if(distance <= shootRange)
        {
            Shoot();
        }
        else if(distance <= detectionRange)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }



    void Patrol()
    {
        animator.SetBool("IsWalking", true);

        transform.position =
        Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            patrolSpeed * Time.deltaTime);


        if(Vector2.Distance(
            transform.position,
            targetPoint.position) < 0.1f)
        {
            targetPoint =
            targetPoint == pointA ? pointB : pointA;
        }


        Flip(targetPoint.position.x -
        transform.position.x);
    }



    void Chase()
    {
        animator.SetBool("IsWalking", true);


        transform.position =
        Vector2.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime);


        Flip(player.position.x -
        transform.position.x);
    }



    void Attack()
    {
        rb.linearVelocity = Vector2.zero;

        animator.SetBool("IsWalking", false);


        if(Time.time < nextAttack)
            return;


        nextAttack =
        Time.time + attackCooldown;


        animator.SetTrigger("IsAttacking");


        PlayerHealth hp =
        player.GetComponent<PlayerHealth>();


        if(hp != null)
            hp.TakeDamage(attackDamage);
    }




    void Shoot()
    {
        rb.linearVelocity = Vector2.zero;

        animator.SetBool("IsWalking", false);


        if(Time.time < nextShoot)
            return;


        nextShoot =
        Time.time + shootCooldown;


        animator.SetTrigger("IsShooting");
    }



    public void FireBullet()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            shootPoint.position,
            Quaternion.identity
        );


        EnemyBullet bulletScript =
        bullet.GetComponent<EnemyBullet>();


        bulletScript.SetDirection(facingLeft);
    }


    void Flip(float direction)
    {
        if(direction > 0)
        {
            spriteRenderer.flipX = false;
            facingLeft = false;
        }
        else if(direction < 0)
        {
            spriteRenderer.flipX = true;
            facingLeft = true;
        }
    }
}