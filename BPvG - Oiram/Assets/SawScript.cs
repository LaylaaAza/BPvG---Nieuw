using UnityEngine;

public class SawEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float leftBound = -5f;
    public float rightBound = 5f;
    private int direction = 1;

    private void Update()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (transform.position.x >= rightBound || transform.position.x <= leftBound)
        {
            direction *= -1; // Reverse direction
            FlipSaw(direction); // Flip the saw sprite to match the direction
        }
    }

    private void FlipSaw(int dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir; // Flip the x-scale to match direction
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.Respawn();
            }
        }
    }
}
