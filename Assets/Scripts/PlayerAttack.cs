using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ataque")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float cooldown = 0.3f;

    private float nextAttackTime;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D playerCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Time.time < nextAttackTime)
            return;

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            Shoot();

            nextAttackTime = Time.time + cooldown;
        }
    }

   void Shoot()
    {
        animator.SetTrigger("Shoot");

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity);

        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectileScript.SetDirection(spriteRenderer.flipX);

        Physics2D.IgnoreCollision(
            projectile.GetComponent<Collider2D>(),
            playerCollider);
}
}