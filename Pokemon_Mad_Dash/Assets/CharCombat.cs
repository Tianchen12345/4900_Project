using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCombat : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public float AttackRange=0.5f;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
          Attack();
        }
    }
    void Attack()
    {
      //attack trigger
      animator.SetTrigger("LightAttack");

      //Detect enemies hit
      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position,AttackRange,enemyLayers);

      //
      foreach(Collider2D enemy in hitEnemies)
      {
        Debug.Log("We Hit " + enemy.name);
      }
    }

    void OnDrawGizmosSelected()
    {


      Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);

    }
}
