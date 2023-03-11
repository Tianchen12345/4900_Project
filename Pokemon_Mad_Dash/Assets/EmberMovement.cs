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

  private void Start(){

    Invoke("DestroyProjectile", lifeTime);
  }
  private void Update()
  {
    RaycastHit2D hitInfo=Physics2D.Raycast(transform.position,transform.right,distance);
    if(hitInfo.collider!= null){
      if(hitInfo.collider.CompareTag("Enemy")){
        hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
        DestroyProjectile();
      }

    }
    rb.velocity = transform.right * speed;

  }
  void DestroyProjectile(){

    Destroy(gameObject);
  }

}
