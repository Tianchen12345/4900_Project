using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chikorita : MonoBehaviour
{
    [SerializeField] GameObject razorLeaf;

    Animator myAnimator;
    Rigidbody2D myRigidbody;

    private bool isAttacking = false;
    private float attackTime = 3f;

    [SerializeField] float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
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
        float sign = Mathf.Sign(myRigidbody.velocity.x);
        transform.localScale = new Vector2(sign, 1f);

    }


    private bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }
    public void Attack()
    {
        EnemyMovement();
        if (!isAttacking)
        {
            myAnimator.SetTrigger("Attack");
            isAttacking = true;
            StartCoroutine(WaitASecond(4.5f));
            Vector2 direction = new Vector2(-transform.localScale.x, 0);
            GameObject rL = Instantiate(razorLeaf, transform.position, Quaternion.identity);
            rL.GetComponent<RazorLeaf>().SetDirection(direction);
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }

    private IEnumerator WaitASecond(float f)
    {
        yield return new WaitForSeconds(f);
    }
}
