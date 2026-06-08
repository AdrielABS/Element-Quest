using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawns : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Transform mainSpawn;

    private Vector3 respawnPoint;
    private Rigidbody2D rb;

    private bool fallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        respawnPoint = mainSpawn.position;
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
        fallen = true;
        Debug.Log("Você caiu!");
    }
}
}