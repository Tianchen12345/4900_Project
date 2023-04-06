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
        Debug.Log(player.position);
        transform.position = Vector2.MoveTowards(this.transform.position,player.position, speed * Time.deltaTime);
      }
      else if (distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
      {
        Instantiate(bullet, AttackPoint.transform.position, Quaternion.identity);
        nextFireTime = Time.time + fireRate;
      }
    }

    private void OnDrawGizmosSelected(){
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(transform.position,lineOfSite);
      Gizmos.DrawWireSphere(transform.position,shootingRange);
    }
}
