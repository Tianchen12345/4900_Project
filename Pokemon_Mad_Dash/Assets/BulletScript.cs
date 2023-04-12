using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
  GameObject target;
  [SerializeField] private string targetTag;
  public float speed;
  Rigidbody2D bulletRB;
    [SerializeField]private int damage = 15;
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized* speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);
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
      DestroyProjectile();
    }
}
