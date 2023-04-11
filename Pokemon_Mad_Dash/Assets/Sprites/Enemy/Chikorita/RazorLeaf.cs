using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorLeaf : MonoBehaviour
{
    // Public fields
    public float speed = 10f;   // The speed of the razor leaf
    public float lifespan = 3f; // The lifespan of the razor leaf in seconds

    // Private fields
    private Rigidbody2D myRigidbody2D;     // The rigidbody of the razor leaf
    private Vector2 direction;  // The direction of the razor leaf's movement

    private void Start()
    {
        // Get the rigidbody component of the razor leaf
        myRigidbody2D = GetComponent<Rigidbody2D>();

        // Set the direction of the razor leaf to the right
        direction = Vector2.right;

        // Destroy the razor leaf after its lifespan has expired
        Destroy(gameObject, lifespan);
    }

    private void FixedUpdate()
    {
        // Move the razor leaf in its current direction at a constant speed
        myRigidbody2D.MovePosition(myRigidbody2D.position + Vector2.right * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the razor leaf collides with an enemy, damage the enemy and destroy the razor leaf
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.BeAttacked();
            Destroy(gameObject);
        }
    }
}
