using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

public CharMovement charMovement1;
public CharMovement charMovement2;
public int maxHealth= 50;
public int damage = 10;
int currentHealth;
public bool isBoss1 = false;
public UnityEvent<float> OnHealthChange;
public bool isChar1 = false;
public bool isChar2 = true;
  // Start is called before the first frame update
  void Start()
  {
      currentHealth=maxHealth;
  }

  // Update is called once per frame
  public void TakeDamage(int damage)
  {
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
      if(isChar2)
      {
        charMovement2.KBCounter = charMovement2.KBTotalTime;
        charMovement2.TakeDamage(damage);
        if(collision.transform.position.x <= transform.position.x)
        {
          charMovement2.KnockFromRight = true;
        }
        if(collision.transform.position.x > transform.position.x)
        {
          charMovement2.KnockFromRight = false;
        }
      }
      if(isChar1)
      {
        charMovement1.KBCounter = charMovement1.KBTotalTime;
        charMovement1.TakeDamage(damage);
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

  }




}
