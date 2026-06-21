using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int Health;

    private PlayerMovement playerMovement;
    private bool canTakeDamage = true;

    private void Start()
    {
        Health = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    public void TakeDamage(int damage)
    {
        if (!canTakeDamage)
            return;

        Health -= damage;

        StartCoroutine(DamageCooldown());

        if (Health <= 0)
        {
        playerMovement.Die();
        }
    }

    IEnumerator DamageCooldown()
{
    canTakeDamage = false;

    yield return new WaitForSeconds(1f);

    canTakeDamage = true;
}
}