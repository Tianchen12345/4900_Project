using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharHealth : MonoBehaviour
{
    public int health;
    public int maxHealth= 30;
    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
    }

    public void TakeDamage(int damage){
      health -=damage;

      animator.SetTrigger("TakeDamage");

      if(health<=0){
        Destroy(gameObject);
      }
    }
}
