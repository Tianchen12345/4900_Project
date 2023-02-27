using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{

    [SerializeField] float movement;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] int speed;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool jumpPressed = false;
    [SerializeField] float jumpForce = 500.0f;
    [SerializeField] bool isGrounded = true;

    public Animator animator;

    public int health;
    public int maxHealth= 30;

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
      movement = Input.GetAxis("Horizontal");
      animator.SetFloat("Speed", Mathf.Abs(movement));
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
       if(KnockFromRight == true)
       {
         rigid.velocity= new Vector2(-KBForce,KBForce);
       }
       if(KnockFromRight == false)
       {
         rigid.velocity=new Vector2(KBForce,-KBForce);
       }
       KBCounter -=Time.deltaTime;
     }

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
   }
   public void TakeDamage(int damage){
     health -=damage;

     animator.SetTrigger("TakeDamage");

     if(health<=0){
       Destroy(gameObject);
     }
   }
}
