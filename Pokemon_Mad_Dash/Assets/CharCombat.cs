using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCombat : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public float AttackRange=0.5f;
    public LayerMask enemyLayers;

    public int lightDamage=20;
    public float attackRate= 2f;
    float nextAttackTime=0f;

    public GameObject SpecialAttackPrefab;
    private float timeBtwShots;
    private float startTimeBtwShot;
    // Update is called once per frame
    void Update()
    {
      if(Time.time >= nextAttackTime){
        if(Input.GetKeyDown(KeyCode.Q))
        {
          //Attack();
          animator.SetTrigger("LightAttack");
          nextAttackTime=Time.time+ 1f / attackRate;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
          SpecialAttack();
          //animator.SetTrigger("SpecialAttack");
          nextAttackTime=Time.time+ 1f / attackRate;
        }
      }
    }
    void Attack()
    {
      //attack trigger
      //animator.SetTrigger("LightAttack");

      //Detect enemies hit
      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position,AttackRange,enemyLayers);

      //
      foreach(Collider2D enemy in hitEnemies)
      {
        Debug.Log("We Hit " + enemy.name);
        enemy.GetComponent<Enemy>().TakeDamage(lightDamage);
      }
    
    }

    void SpecialAttack()
    {

      //Instantiate(SpecialAttackPrefab, AttackPoint.position, AttackPoint.rotation);
      animator.SetTrigger("SpecialAttack");
      Instantiate(SpecialAttackPrefab, AttackPoint.position, AttackPoint.rotation);


    }
    void OnDrawGizmosSelected()
    {


      Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);

    }
}
