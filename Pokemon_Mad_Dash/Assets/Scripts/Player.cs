using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float runSpeed = 10f;  // The player's movement speed on the ground
    [SerializeField] float jumpSpeed = 13f; // The player's jump speed
    [SerializeField] float climbSpeed = 8f; // The player's climbing speed
    [SerializeField] float attackRadius = 2f;   // player attack range
    [SerializeField] Vector2 beAttacked = new Vector2(10f, 20f);    // The force and direction of knockback when hit
    [SerializeField] Transform hurtBox; // The center position of the player's hurt box for attacking
    [SerializeField] Transform AttackPoint;
    [SerializeField] SpriteRenderer Explosion;

    [Header("Sound Effects")]
    [SerializeField] AudioClip jumpingSFX, attackingSFX, beAttackedSFX, runningSFX; // SFX is Sound effects

    // Components of the player object
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2D;
    PolygonCollider2D mypolygonCollider2D;
    AudioSource myAudioSource;


    float MyGravityScale;   // Gravity scale of the player's rigidbody
    bool injured = false;   // player injury state

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        mypolygonCollider2D = GetComponent<PolygonCollider2D>();
        myAudioSource = GetComponent<AudioSource>();

        MyGravityScale = myRigidbody2D.gravityScale;

        myAnimator.SetTrigger("Exit Door"); // Trigger the "Exit Door" animation state at the begining of game
    }

    // Update is called once per frame
    void Update()
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("NPC")))
        {
            if (DialogueManager.GetInstance().storyIsPlaying)
            {
                return;
            }
        }

        // Only allow player actions if not injured
        if (!injured)
        {
            Run();
            Jump();
            Climb();

            // Check for collision with enemy or trap layers to trigger injury
            if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                BeAttacked();
            }
            if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Trap")))
            {
                BeAttacked();
            }
        }
        // Check for collision with interactable layer to trigger enter door animation
        ExitLevel();
    }


    #region Load and Exit level
    // Check for collision with interactable layer to trigger enter door animation
    private void ExitLevel()
    {
        if (!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Interactable"))) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Vertical"))
        {
            myAnimator.SetTrigger("Enter Door");
        }
    }

    // Load the next level when player enters door
    public void LoadNextLevel()
    {
        FindObjectOfType<ExitDoor>().StartLoadingNextLevel();
        TurnOffRenderer();
    }

    // Turn off player sprite renderer
    private void TurnOffRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    #endregion

    // The player is attacked
    public void BeAttacked()
    {
        // Add a thrust to the player
        myRigidbody2D.velocity = beAttacked * new Vector2(-transform.localScale.x, 1f);

        myAnimator.SetTrigger("Be Attacked");
        injured = true;
        myAudioSource.PlayOneShot(beAttackedSFX);

        // Take the player's life, if not enough, reset the game
        FindObjectOfType<GameSession>().ProcessPlayerDeath();

        StartCoroutine(recoveryFromInjury());
    }

    // continue when player recovers from injury
    IEnumerator recoveryFromInjury()
    {
        yield return new WaitForSeconds(0.5f);
        injured = false;
    }

    #region Player action
    // Allow the player to climb when the player touches the ladder
    private void Climb()
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbingVelocity = new Vector2(myRigidbody2D.velocity.x, controlThrow * climbSpeed);

            myRigidbody2D.velocity = climbingVelocity;
            myRigidbody2D.gravityScale = 0f;    // Disable gravity to make the player climb up the ladder
        }
        else
        {
            myRigidbody2D.gravityScale = MyGravityScale;    // If the player is not touching a ladder, enable gravity again
        }
    }

    private void Jump()
    {
        // Check if the player is touching the ground
        if (!(mypolygonCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))) { return; }

        bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");

        if (isJumping)
        {
            Vector2 jumpVelocity = new Vector2(myRigidbody2D.velocity.x, jumpSpeed);
            myRigidbody2D.velocity = jumpVelocity;

            myAudioSource.PlayOneShot(jumpingSFX);
        }
    }

    // Allows the player to run when the player presses the left and right buttons
    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;
                
        FlipSprites();
        ChangingToRunningState();
    }

    // Play running SFX when the player is running
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
            float horizontalVelocity = myRigidbody2D.velocity.x;
            transform.localScale = new Vector3(Mathf.Sign(horizontalVelocity), 1f, 1f);
            
            if (horizontalVelocity != 0)
            {
                float rotationY = horizontalVelocity < 0 ? 180f : 0f;
                AttackPoint.transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
                if (rotationY == 180)
                {
                    Explosion.flipX = false;
                }
                else
                {
                    Explosion.flipX = true;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hurtBox.position, attackRadius);
    }
    #endregion
    #region Change Sound Volume
    public void ChangeSoundVolume()
    {
        //get the initial volume of Sound and Change it
        float currentVolume = PlayerPrefs.GetFloat("soundVolume");
        currentVolume += 0.2f;

        //check if the volume reach the maximum and minimum
        if (currentVolume < 0)
        {
            currentVolume = 1;
        }
        else if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        //assign final volume
        myAudioSource.volume = currentVolume;

        PlayerPrefs.SetFloat("soundVolume", currentVolume);
    }
    #endregion
}
