using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 13f;
    [SerializeField] float climbSpeed = 8f;
    [SerializeField] float attackRadius = 2f;
    [SerializeField] Vector2 beAttacked = new Vector2(10f, 30f);
    [SerializeField] Transform hurtBox;
    [SerializeField] AudioClip jumpingSFX, attackingSFX, beAttackedSFX, runningSFX;

    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2D;
    PolygonCollider2D mypolygonCollider2D;
    AudioSource myAudioSource;


    float MyGravityScale;
    bool injured = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        mypolygonCollider2D = GetComponent<PolygonCollider2D>();
        myAudioSource = GetComponent<AudioSource>();
        
        MyGravityScale = myRigidbody2D.gravityScale;

        myAnimator.SetTrigger("Exit Door");


    }

    // Update is called once per frame
    void Update()
    {
        if (!injured)
        {
            Run();
            Attack();
            Jump();
            Climb();
            if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                BeAttacked();
            }
        }

        ExitLevel();
    }

    private void ExitLevel()
    {
        if (!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Interactable"))){   return; }

        if (CrossPlatformInputManager.GetButtonDown("Vertical"))
        {
            myAnimator.SetTrigger("Enter Door");
        }
    }

    public void LoadNextLevel()
    {
        FindObjectOfType<ExitDoor>().StartLoadingNextLevel();
        TurnOffRenderer();
    }

    private void TurnOffRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void BeAttacked()
    {
        myRigidbody2D.velocity = beAttacked * new Vector2(-transform.localScale.x, 1f);

        myAnimator.SetTrigger("Be Attacked");
        injured = true;
        myAudioSource.PlayOneShot(beAttackedSFX);

        FindObjectOfType<GameSession>().ProcessPlayerDeath();

        StartCoroutine(recoveryFromInjury());
    }

    IEnumerator recoveryFromInjury()
    {
        yield return new WaitForSeconds(1f);

        injured = false;
    }

    private void Climb()
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbingVelocity = new Vector2(myRigidbody2D.velocity.x, controlThrow * climbSpeed);

            myRigidbody2D.velocity = climbingVelocity;

            myRigidbody2D.gravityScale = 0f;
        }
        else
        {
            myRigidbody2D.gravityScale = MyGravityScale;
        }
    }

    private void Jump()
    {

        if (!(mypolygonCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) || mypolygonCollider2D.IsTouchingLayers(LayerMask.GetMask("Platform"))))
        {
            return;
        }

        bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");

        if (isJumping)
        {
            Vector2 jumpVelocity = new Vector2(myRigidbody2D.velocity.x, jumpSpeed);
            myRigidbody2D.velocity = jumpVelocity;

            myAudioSource.PlayOneShot(jumpingSFX);
        }
    }

    // Running is a method for running
    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;

        FlipSprites();

        ChangingToRunningState();
        Attack();
    }

    void PlayRunningSFX()
    {
        bool playerMovesHorizontally = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        if (playerMovesHorizontally)
        {
            if (mypolygonCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myAudioSource.PlayOneShot(runningSFX);
            }
        }
        else
        {
            myAudioSource.Stop();
        }
    }

    private void Attack()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            myAnimator.SetTrigger("Attacking");
            myAudioSource.PlayOneShot(attackingSFX);

            Collider2D[] enemyToHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Enemy"));

            foreach(Collider2D enemy in enemyToHit)
            {
                enemy.GetComponent<Nidoran>().Dying();
            }
        }
        
    }

    private void ChangingToRunningState()
    {
        bool runningHorizontally = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", runningHorizontally);
    }

    // make the sprites flip when user press left button
    private void FlipSprites()
    {
        bool runningHorizontally = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        if (runningHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hurtBox.position, attackRadius);
    }
}
