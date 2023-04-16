using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player2 : MonoBehaviour {

    private Vector2 moveVelocity;

    [SerializeField] float speed = 10f;  // The player's movement speed on the ground
    [SerializeField] float jumpSpeed = 13f; // The player's jump speed
    [SerializeField] Vector2 beAttacked = new Vector2(10f, 20f);    // The force and direction of knockback when hit

    [Header("Sound Effects")]
    [SerializeField] AudioClip beAttackedSFX, runningSFX, jumpingSFX; // SFX is Sound effects

    Animator myAnimator;
    Rigidbody2D myRigidbody2D;
    BoxCollider2D myBoxCollider2D;
    PolygonCollider2D mypolygonCollider2D;
    AudioSource myAudioSource;

    float MyGravityScale;   // Gravity scale of the player's rigidbody
    bool injured = false;   // player injury state


    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        mypolygonCollider2D = GetComponent<PolygonCollider2D>();
        myAudioSource = GetComponent<AudioSource>();

        MyGravityScale = myRigidbody2D.gravityScale;
    }

    private void Update()
    {
        if (!injured)
        {
            Run();
            Jump();

            // Check for collision with enemy or trap layers to trigger injury
            if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                BeAttacked();
            }
        }
    }

    public void BeAttacked()
    {
        // Add a thrust to the player
        myRigidbody2D.velocity = beAttacked * new Vector2(-transform.localScale.x, 1f);

        myAnimator.SetTrigger("getHitted");
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

        Vector2 playerVelocity = new Vector2(controlThrow * speed, myRigidbody2D.velocity.y);
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
        myAnimator.SetBool("isRunning", runningHorizontally);
    }

    // make the sprites flip when user press left button
    private void FlipSprites()
    {
        bool runningHorizontally = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        if (runningHorizontally)
        {
            float horizontalVelocity = myRigidbody2D.velocity.x;
            transform.localScale = new Vector3(Mathf.Sign(horizontalVelocity), 1f, 1f);
        }
    }

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
