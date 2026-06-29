using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int Health;
    public Image[] Heart;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;   

    private PlayerMovement playerMovement;
    private bool canTakeDamage = true;

    private void Start()
    {
        Health = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
        HearthUpdate();
    }
    
    public void TakeDamage(int damage)
    {
        if (!canTakeDamage)
            return;

        Health -= damage;

        HearthUpdate();

        StartCoroutine(DamageCooldown());

        if (Health <= 0)
        {
            playerMovement.Die();
        }
    }
    public void HearthUpdate()
    {
       if (Health == 6)
        {
            Heart[0].sprite = fullHeart;
            Heart[1].sprite = fullHeart;
            Heart[2].sprite = fullHeart;
        }
        else if (Health == 5)
        {
            Heart[0].sprite = fullHeart;
            Heart[1].sprite = fullHeart;
            Heart[2].sprite = halfHeart;
        }
        else if (Health == 4)
        {
            Heart[0].sprite = fullHeart;
            Heart[1].sprite = fullHeart;
            Heart[2].sprite = emptyHeart;
        }
        else if (Health == 3)
        {
            Heart[0].sprite = fullHeart;
            Heart[1].sprite = halfHeart;
            Heart[2].sprite = emptyHeart;
        }
        else if (Health == 2)
        {
            Heart[0].sprite = fullHeart;
            Heart[1].sprite = emptyHeart;
            Heart[2].sprite = emptyHeart;
        }
        else if (Health == 1)
        {
            Heart[0].sprite = halfHeart;
            Heart[1].sprite = emptyHeart;
            Heart[2].sprite = emptyHeart;
        }
        else
        {
            Heart[0].sprite = emptyHeart;
            Heart[1].sprite = emptyHeart;
            Heart[2].sprite = emptyHeart;
        }
    }

    IEnumerator DamageCooldown()
    {
    canTakeDamage = false;

    yield return new WaitForSeconds(1f);

    canTakeDamage = true;
    }
}