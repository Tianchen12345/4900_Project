using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
public CharMovement charMovement;
public int maxHealth= 50;
public int damage = 10;
int currentHealth;

  // Start is called before the first frame update
  void Start()
  {
      currentHealth=maxHealth;
  }

  // Update is called once per frame
  public void TakeDamage(int damage)
  {
    currentHealth -= damage;
     if(currentHealth <=0)
     {

       Die();
     }

  }

  void Die()
  {

   Debug.Log("enemy die");
    GetComponent<Collider2D>().enabled = false;
    GetComponent<SpriteRenderer>().enabled =false;
    //this.enabled = false;
  }
  private void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag == "Player"){
      //Debug.Log(damage);
      charMovement.KBCounter = charMovement.KBTotalTime;
      if(collision.transform.position.x <= transform.position.x){
        charMovement.KnockFromRight = true;
      }
      if(collision.transform.position.x > transform.position.x){
        charMovement.KnockFromRight = false;
      }
      charMovement.TakeDamage(damage);
    }
  }
}
