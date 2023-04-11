using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chikorita : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    [SerializeField] GameObject razorLeaf;
    [SerializeField] Transform attackPoint;

    Animator myAnimator;

    private bool isAttacking = false;
    private float attackTime = 3f;

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        speed = GetComponent<Enemies>().speed;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        EnemyMovement();
        speed = GetComponent<Enemies>().speed;        
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
    public void Attack()
    {
        if (!isAttacking)
        {
            myAnimator.SetTrigger("Attack");
            isAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        GameObject Razor = Instantiate(this.razorLeaf, attackPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(attackTime);
        Destroy(Razor);
        isAttacking = false;
    }
}
