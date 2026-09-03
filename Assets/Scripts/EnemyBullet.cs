using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 12f;
    public int damage = 1;
    public float lifeTime = 4f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(bool facingLeft)
    {
        float direction = facingLeft ? -1f : 1f;

        rb.linearVelocity = new Vector2(direction * speed, 0);

        spriteRenderer.flipX = facingLeft;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if (!collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}