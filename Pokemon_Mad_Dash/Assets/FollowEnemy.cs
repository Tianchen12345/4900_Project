using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowEnemy : MonoBehaviour
{

    private Transform player;

    public float speed;
    public float lineOfSite;
    public bool FacingRight = false;

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

    }

    void Flip(){
      FacingRight = !FacingRight;
      Vector3 tmpScale = gameObject.transform.localScale;
      tmpScale.x *= -1;
      gameObject.transform.localScale = tmpScale;
    }
}
