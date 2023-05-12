using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmberMovement : MonoBehaviour
{

    public int damage = 30;
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public Rigidbody2D rb;
    [SerializeField] GameObject impactEffect;

  private void Start(){

    Invoke("DestroyProjectile", lifeTime);
  }
  private void Update()
  {
    /*
    RaycastHit2D hitInfo=Physics2D.Raycast(transform.position,transform.right,distance);

    if(hitInfo.collider!= null){

      if(hitInfo.collider.CompareTag("Enemy")){
        Debug.Log(hitInfo);
        hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
        DestroyProjectile();
      }

    }
    */
    rb.velocity = transform.right * speed;



  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.tag == "Enemy"){
      collision.GetComponent<Enemy>().TakeDamage(damage);
      Instantiate(impactEffect, transform.position, Quaternion.identity);
      DestroyProjectile();
    }
    if(collision.tag == "Ground"){
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        DestroyProjectile();
    }
    if(collision.tag == "Bullet"){
      collision.GetComponent<BulletScript>().TakeDamage(damage);
    }
    if(collision.tag == "HomingBullet"){
      collision.GetComponent<HomingBullet>().TakeDamage(damage);
      //DestroyProjectile;
    }

  }

  void DestroyProjectile(){

    Destroy(gameObject);
  }

}
