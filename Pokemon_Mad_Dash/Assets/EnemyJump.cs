using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
  public Transform [] patrolPoints;
  public float speed;
  public int PatrolDestination;
  public Rigidbody2D rb;
  [SerializeField] bool isGrounded = true;
  public float jumpforce = 1000f;
  // Start is called before the first frame update


  // Update is called once per frame
  void Update()
  {
    if(isGrounded){
      isGrounded = false;
      rb.AddForce(Vector2.up* jumpforce);
    }
      if(PatrolDestination == 0)
      {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position,patrolPoints[0].position)<.2f)
        {
          transform.localScale = new Vector3(1,1,1);
          PatrolDestination =1;
        }
      }
      if(PatrolDestination == 1)
      {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position,patrolPoints[1].position)<.2f)
        {
          transform.localScale = new Vector3(-1,1,1);
          PatrolDestination =0;
        }
      }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
      if (collision.gameObject.tag == "Ground")
      {
          isGrounded = true;
      }
  }
}
