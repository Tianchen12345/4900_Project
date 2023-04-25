using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
//public GameObject [] characters;


public CharMovement charMovement1;
//public CharMovement charMovement2;
public int maxHealth= 50;
public int damage = 10;
public int currentHealth;
public bool isBoss1 = false;
public bool isBoss2 = false;
public bool isBoss3 = false;
public UnityEvent<float> OnHealthChange;
public bool isInvulerable = false;
//public bool isChar1 = true;
//public bool isChar2 = false;
  // Start is called before the first frame update
  void Start()
  {
      currentHealth=maxHealth;
  }

  // Update is called once per frame
  public void TakeDamage(int damage)
  {
    if(isInvulerable){
      return;
    }
    currentHealth -= damage;
    OnHealthChange?.Invoke((float)currentHealth / maxHealth);
     if(currentHealth <=0)
     {

       Die();
     }
     if(isBoss1 && currentHealth <= 100)
     {
       GetComponent<EnemyPatrolMovement>().enabled = false;
       GetComponent<EnemyJump>().enabled = true;
       GetComponent<Animator>().SetBool("IsJumping", true);
     }
     if(isBoss2 && currentHealth <= 150)
     {
       GetComponent<FollowAndShootEnemy>().enabled = false;
       GetComponent<HomingShootingEnemy>().enabled = true;
     }
     if(isBoss3){
       GetComponent<Animator>().enabled = true;
     }
     if(isBoss3 && currentHealth <= 300){
       GetComponent<Animator>().SetTrigger("transform1");
       GetComponent<FollowEnemy>().enabled = false;

     }
     if(isBoss3 && currentHealth <= 200){
       GetComponent<Animator>().SetTrigger("transform2");
       GetComponent<EnemyPatrolMovement>().enabled = true;
     }
     if(isBoss3 && currentHealth <=100){
       GetComponent<Animator>().SetTrigger("transform3");
       GetComponent<EnemyPatrolMovement>().enabled = false;
       GetComponent<FollowAndShootEnemy>().enabled = true;
     }
  }

  void Die()
  {

   Debug.Log("enemy die");
    Destroy(gameObject);
    GetComponent<Collider2D>().enabled = false;
    GetComponent<SpriteRenderer>().enabled =false;
    GetComponent<Animator>().enabled = false;

    //this.enabled = false;
  }
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.tag == "Player")
    {
        charMovement1.TakeDamage(damage);
        StartCoroutine(IgnoreCollision());
        charMovement1.KBCounter = charMovement1.KBTotalTime;

        if(collision.transform.position.x <= transform.position.x)
        {
          charMovement1.KnockFromRight = true;
        }
        if(collision.transform.position.x > transform.position.x)
        {
          charMovement1.KnockFromRight = false;
        }


  }

  }
  private IEnumerator IgnoreCollision(){

    Physics2D.IgnoreCollision(charMovement1.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>(), true);


    yield return new WaitForSeconds(1);

    Physics2D.IgnoreCollision(charMovement1.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>(), false);

  }

}
