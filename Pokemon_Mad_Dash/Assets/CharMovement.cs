using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class CharMovement : MonoBehaviour
{

    [SerializeField] float climbSpeed = 8f;
    [SerializeField] float movement;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] int speed;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool jumpPressed = false;
    [SerializeField] float jumpForce = 1500.0f;
    [SerializeField] bool isGrounded = true;

    public bool door = false;
    public Animator animator;
    [SerializeField] CapsuleCollider2D myCollider2D;

    public int health;
    public int maxHealth= 30;
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
      if (rigid == null)
          rigid = GetComponent<Rigidbody2D>();
      speed = 15;
      health=maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
      Run();
      Climb();
      ExitLevel();
      if (Input.GetButtonDown("Jump"))
          jumpPressed = true;

    }

    void FixedUpdate()
   {
     if(KBCounter <=0)
     {
       rigid.velocity = new Vector2(movement * speed, rigid.velocity.y);
       if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
           Flip();
       if (jumpPressed && isGrounded)

           Jump();
     }
     else{
       if(KnockFromRight)
       {
         rigid.velocity= new Vector2(-KBForce,KBForce);
       }
       if(!KnockFromRight)
       {
         rigid.velocity=new Vector2(KBForce,KBForce);
       }
       KBCounter -=Time.deltaTime;
     }

   }

   void Run(){
     movement = Input.GetAxis("Horizontal");
     Vector2 playerVelocity = new Vector2(movement * speed, rigid.velocity.y);
     rigid.velocity = playerVelocity;
     animator.SetFloat("Speed", Mathf.Abs(movement));
   }
   void Flip()
   {
       transform.Rotate(0, 180, 0);
       isFacingRight = !isFacingRight;
   }

   void Jump()
   {
       rigid.velocity = new Vector2(rigid.velocity.x, 0);
       rigid.AddForce(new Vector2(0, jumpForce));
       isGrounded = false;
       jumpPressed = false;
       animator.SetBool("isJumping",true);
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.gameObject.tag == "Ground")
       {
           isGrounded = true;
           animator.SetBool("isJumping", false);
       }
       if(collision.gameObject.layer == 13){
         TakeDamage(20);
         KBCounter = KBTotalTime;

            if(collision.transform.position.x <= transform.position.x)
            {
              KnockFromRight = true;
            }
            if(collision.transform.position.x > transform.position.x)
            {
              KnockFromRight = false;
            }
   }

 }
   public void TakeDamage(int damage){
     if(isInvincible) return;

     health -=damage;
     OnHealthChange?.Invoke((float)health / maxHealth);
     animator.SetTrigger("TakeDamage");

     if(health<=0){

       //Destroy(gameObject);
       var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(currentSceneIndex);
     }
      StartCoroutine(BecomeTemporarilyInvincible());
   }

   private IEnumerator BecomeTemporarilyInvincible()
{

    isInvincible = true;

    yield return new WaitForSeconds(invincibilityDurationSeconds);

    isInvincible = false;


}
private void ExitLevel()
{
    if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Interactable"))) { return; }

    if (Input.GetButtonDown("Vertical"))
    {
        animator.SetTrigger("EnterDoor");
    }
}
public void LoadNextLevel()
{
    FindObjectOfType<ExitDoor>().StartLoadingNextLevel();
    GetComponent<SpriteRenderer>().enabled = false;
}


private void Climb()
{
    if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
    {
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbingVelocity = new Vector2(rigid.velocity.x, controlThrow * climbSpeed);

        rigid.velocity = climbingVelocity;
        rigid.gravityScale = 0f;    // Disable gravity to make the player climb up the ladder
    }
    else
    {
        rigid.gravityScale = 1;    // If the player is not touching a ladder, enable gravity again
    }
}

}
