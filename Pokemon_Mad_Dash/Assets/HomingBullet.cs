using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    GameObject target;
    public float speed = 5;
    public bool FacingRight = false;
    public int bulletHP = 1;
    public int lifeTime = 2;
    [SerializeField] private string targetTag;

    [SerializeField]private int damage = 15;
    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player");
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if(target.transform.position.x < gameObject.transform.position.x && FacingRight)
        {
          Flip();
        }
        if(target.transform.position.x > gameObject.transform.position.x && !FacingRight)
        {
          Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.tag == targetTag){
        target.GetComponent<CharMovement>().TakeDamage(damage);
        DestroyProjectile();
      }
      if(collision.gameObject.layer == 16 ){
        TakeDamage(20);
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
    void Flip(){
      FacingRight = !FacingRight;
      Vector3 tmpScale = gameObject.transform.localScale;
      tmpScale.x *= -1;
      gameObject.transform.localScale = tmpScale;
    }
}
