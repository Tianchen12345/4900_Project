using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndShootEnemy : MonoBehaviour
{

    private Transform player;

    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    public float nextFireTime;
    public GameObject bullet;
    public GameObject AttackPoint;
    public bool FacingRight = false;
    void Start()
    {
      player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
      float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
      if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
      {
        
        transform.position = Vector2.MoveTowards(this.transform.position,player.position, speed * Time.deltaTime);
      }
      if (distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
      {
        Instantiate(bullet, AttackPoint.transform.position, Quaternion.identity);
        nextFireTime = Time.time + fireRate;
      }
      if(player.transform.position.x < gameObject.transform.position.x && FacingRight)
      {
        Flip();
      }
      if(player.transform.position.x > gameObject.transform.position.x && !FacingRight)
      {
        Flip();
      }
    }

    private void OnDrawGizmosSelected(){
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(transform.position,lineOfSite);
      Gizmos.DrawWireSphere(transform.position,shootingRange);
    }
    void Flip(){
      FacingRight = !FacingRight;
      Vector3 tmpScale = gameObject.transform.localScale;
      tmpScale.x *= -1;
      gameObject.transform.localScale = tmpScale;
    }
}
