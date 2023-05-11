using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caterpie : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        speed = GetComponent<Enemies>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        speed = GetComponent<Enemies>().speed;
        EnemyMovement();
    }

    //Move the enemy character left or right based on its direction and speed
    private void EnemyMovement()
    {
        if (GetComponent<BoxCollider2D>().enabled)
        {
            if (IsFacingLeft())
            {
                myRigidbody.velocity = new Vector2(-speed, 0f);
            }
            else
            {
                myRigidbody.velocity = new Vector2(speed, 0f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprites();
    }

    // Flip the character sprites based on its current velocity direction
    private void FlipSprites()
    {
        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    // Check if the character is facing left
    private bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }
}
