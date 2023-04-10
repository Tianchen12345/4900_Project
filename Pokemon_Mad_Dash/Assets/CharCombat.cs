using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public float mana = 100f;
    public float maxMana = 100f;
    public float manaCost = 10f;
    public float manaRegen = 1f;
    public UnityEvent<float> OnManaChange;

    void Start(){

      mana = maxMana;
    }
    // Update is called once per frame
    void Update()
    {
      if(Time.time >= nextAttackTime){
        if(Input.GetMouseButtonDown(0))
        {
          Attack();
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
      mana += manaRegen * Time.deltaTime;
      OnManaChange?.Invoke((float)mana / maxMana);
      if(mana > maxMana){
        mana = maxMana;
      }
      if(mana<0){
        mana = 0;
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

          if(enemy.tag == "Bullet"){
            enemy.GetComponent<Projectile>().TakeDamage(lightDamage);

          }
        else{
        enemy.GetComponent<Enemy>().TakeDamage(lightDamage);
      }

      }

    }

    void SpecialAttack()
    {

      //Instantiate(SpecialAttackPrefab, AttackPoint.position, AttackPoint.rotation);
        animator.SetTrigger("SpecialAttack");
        useMana();





    }
    void OnDrawGizmosSelected()
    {


      Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);

    }

    public void useMana(){
      if(mana >= manaCost){
        mana = mana - manaCost;
        OnManaChange?.Invoke( mana / maxMana);
        Instantiate(SpecialAttackPrefab, AttackPoint.position, AttackPoint.rotation);
      }

    }
}
