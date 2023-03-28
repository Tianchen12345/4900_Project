using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int health;
    public float speed;
    private float dazedTime;
    public float startDazedTime;

    private Animator myAnimator;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dazedTime <= 0)
        {
            speed = 5;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if(health <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            myRigidbody.bodyType = RigidbodyType2D.Static;
            StartCoroutine(DestroyEnemy());
        }

        EnemyMovement();
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        health -= damage;
    }

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

    private void FlipSprites()
    {
        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    private bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }
}
