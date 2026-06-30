using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;

    private int currentHealth;

    private Animator animator;

    private EnemyAI enemyAI;

    private bool dead = false;

    void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

        enemyAI = GetComponent<EnemyAI>();
    }

    public void TakeDamage(int damage)
    {
        if (dead)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;

        if (enemyAI != null)
            enemyAI.enabled = false;

        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        animator.SetTrigger("Die");

        Destroy(gameObject, 0.8f);
    }
}