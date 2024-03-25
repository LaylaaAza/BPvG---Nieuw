using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private int coins = 0;
    public void AddCoin()
    {
        coins++;
    }
    
    
    [SerializeField] private float runsSpeed = 5f;

    Vector2 moveInput;

    [SerializeField] private Animator animator;

    Rigidbody2D body;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float jumpingpower = 8f;

    bool enemyHit = false;

    private Vector2 startLocation = Vector3.zero;
    private Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startLocation = body.position;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runsSpeed, body.velocity.y);

    // Reset IsJumping to false each frame, so it only triggers when we actually jump.
    animator.SetBool("IsJumping", false);

    if (moveInput.y > 0 && IsGrounded())
    {
        playerVelocity = new Vector2(moveInput.x, jumpingpower);
        // Trigger the jump animation.
        animator.SetBool("IsJumping", true);
    }
    body.velocity = playerVelocity;

        bool playerMoves = Mathf.Abs(body.velocity.x) > Mathf.Epsilon;
        if (playerMoves)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
        // This will hold the direction the player is currently facing.
        float playerDirection = Mathf.Sign(transform.localScale.x);

        if (playerMoves)
        {
            // Determine the direction by checking the input rather than the velocity.
            float directionInput = Mathf.Sign(moveInput.x);

            // Only flip the character if the direction input has changed and is different from the current facing direction.
            if (directionInput != 0 && directionInput != playerDirection)
            {
                // Flip the character by reversing the x scale.
                transform.localScale = new Vector3(-transform.localScale.x, originalScale.y, originalScale.z);
            }
        }

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("LevelOne");
        }

        if (enemyHit)
        {
            Debug.Log("Geraakt");
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyHit = true;
            Respawn();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyHit = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            // Add to the player's coin count here if needed

            Destroy(other.gameObject); // This destroys the coin
        }
        if (other.gameObject.tag == "Save1")
        {
            startLocation = other.transform.position;
            Debug.Log("Save Point Reached: " + startLocation);
        }
        else if (other.gameObject.tag == "FallDetector")
        {
            Respawn();
        }
        else if (other.gameObject.tag == "Level1Finish")
        {
            // Assuming that the next level is named "LevelTwo" and is the second scene in the build index
            SceneManager.LoadScene("LevelTwo");
            // Alternatively, you can use the build index number, like SceneManager.LoadScene(1);
        }
        else if (other.gameObject.tag == "Level2Finish")
        {
            SceneManager.LoadScene("LevelThree");
        }
    }


    public void Respawn()
    {
        body.position = startLocation; // Move the player to the startLocation
        body.velocity = Vector2.zero; // Reset velocity to prevent falling immediately again
                                      // Any other reset logic you need
    }
}

