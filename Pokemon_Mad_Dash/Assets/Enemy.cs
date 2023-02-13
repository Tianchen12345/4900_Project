using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
public int maxHealth= 50;
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
}
