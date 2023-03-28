using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class TestCharMovement : MonoBehaviour
{

    [SerializeField] float movement;
    [SerializeField] Rigidbody2D myRigidbody2D;
    [SerializeField] int speed;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool jumpPressed = false;
    [SerializeField] float jumpForce = 500.0f;
    [SerializeField] bool isGrounded = true;


    public Animator myAnimator;

    public int health;
    public int maxHealth = 30;
    bool isInvincible = false;
    [SerializeField] private float invincibilityDurationSeconds;
    public UnityEvent<float> OnHealthChange;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;
    // Start is called before the first frame update
    void Start()
    {
        if (myRigidbody2D == null)
            myRigidbody2D = GetComponent<Rigidbody2D>();
        speed = 15;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        myAnimator.SetFloat("Speed", Mathf.Abs(movement));
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;

    }
    void FixedUpdate()
    {
        if (KBCounter <= 0)
        {
            myRigidbody2D.velocity = new Vector2(movement * speed, myRigidbody2D.velocity.y);
            if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
                Flip();
            if (jumpPressed && isGrounded)

                Jump();
        }
        else
        {
            if (KnockFromRight)
            {
                myRigidbody2D.velocity = new Vector2(-KBForce, KBForce);
            }
            if (!KnockFromRight)
            {
                myRigidbody2D.velocity = new Vector2(KBForce, KBForce);
            }
            KBCounter -= Time.deltaTime;
        }

    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    void Jump()
    {
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0);
        myRigidbody2D.AddForce(new Vector2(0, jumpForce));
        isGrounded = false;
        jumpPressed = false;
        myAnimator.SetBool("isJumping", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            myAnimator.SetBool("isJumping", false);
        }
    }
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        health -= damage;
        OnHealthChange?.Invoke((float)health / maxHealth);
        myAnimator.SetTrigger("TakeDamage");

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
        StartCoroutine(BecomeTemporarilyInvincible());
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        isInvincible = false;
    }
}
