using UnityEngine;

public class SkiploomJump : MonoBehaviour
{
    public float moveSpeed = 5f;                // The speed at which the enemy moves
    public float jumpForce = 10f;               // The force with which the enemy jumps
    public float moveDistance = 5f;             // The maximum distance the enemy can move
    public Transform groundCheck;               // A reference to a game object that will check if the enemy is on the ground
    public LayerMask groundMask;                // A mask that defines what is considered as ground for the enemy

    private Rigidbody2D rb;                     // A reference to the enemy's rigidbody component
    private bool isGrounded;                    // A flag to check if the enemy is on the ground
    private float moveDirection = 1f;           // The direction in which the enemy is moving
    private Vector2 startingPosition;           // The enemy's starting position

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;  // Store the enemy's starting position
    }

    void Update()
    {
        // Check if the enemy is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);

        if (GetComponent<BoxCollider2D>().enabled)
        {
            // Move the enemy horizontally
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
            }

            // Flip the enemy's sprite if it changes direction
            if (moveDirection > 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (moveDirection < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            // If the enemy is on the ground, jump
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            // Check if the enemy has moved beyond its maximum distance
            if (Mathf.Abs(transform.position.x - startingPosition.x) >= moveDistance)
            {
                // Change direction and move back to starting position
                moveDirection *= -1f;
                rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
            }
        }

    }
}
