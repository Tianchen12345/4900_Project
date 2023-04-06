using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowEnemy : MonoBehaviour
{

    private Transform player;

    public float speed;
    public float lineOfSite;


    void Start()
    {
      player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
      float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
      if(distanceFromPlayer < lineOfSite)
      {

        transform.position = Vector2.MoveTowards(this.transform.position,player.position, speed * Time.deltaTime);
      }

    }

    private void OnDrawGizmosSelected(){
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(transform.position,lineOfSite);

    }
}
