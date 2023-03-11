using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private float speed;
  [SerializeField] private string targetTag;
  public float lifeTime;
  public float distance;
  private Vector2 direction;
  private int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-direction*speed*Time.deltaTime);
    }
    public void Setup(Vector2 direction){
      this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision){
      if(collision.tag == targetTag){
        collision.GetComponent<CharMovement>().TakeDamage(damage);
        DestroyProjectile();
      }
    }
    void DestroyProjectile(){

      Destroy(gameObject);
    }
}
