using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
  GameObject target;
  [SerializeField] private string targetTag;
  public float speed;
  Rigidbody2D bulletRB;
  public int bulletHP = 1;

    public int damage = 10;
    public int time =2;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized* speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.tag == targetTag){
        target.GetComponent<CharMovement>().TakeDamage(damage);
        DestroyProjectile();
      }
      else{
        DestroyProjectile();
      }
    }

    void DestroyProjectile(){

      Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {

        bulletHP -= damage;
        if(bulletHP<0){
        DestroyProjectile();
        }
      }

}
