using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawns : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Transform mainSpawn;
    public void Respawn()
    {
        transform.position = respawnPoint;
        rb.linearVelocity = Vector2.zero;
    }

    private Vector3 respawnPoint;
    private Rigidbody2D rb;
    private PlayerSpawns playerSpawns;

    private bool fallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (mainSpawn != null)
        {
            respawnPoint = mainSpawn.position;
        }
        else
        {
            Debug.LogError("Main Spawn não foi atribuído!");
        }
}

    void Update()
    {
        if (fallen)
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                ReturnToSpawnPoint();
                fallen = false;
            }
        }
    }

    void ReturnToSpawnPoint()
    {
        transform.position =
            new Vector3(
                respawnPoint.x,
                respawnPoint.y,
                transform.position.z);

        rb.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("FallZone"))
    {
        GameManager.Instance.RestartLevel();
    }
}
}