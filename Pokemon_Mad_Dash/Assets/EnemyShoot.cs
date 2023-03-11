using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour

{
    // Start is called before the first frame update
    [SerializeField] private GameObject xPrefab;
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private Animator animator;
    private int damage = 10;
    public void CollisionEnter(GameObject other){
      other.GetComponent<CharMovement>().TakeDamage(damage);

    }
    private void StopAttack(){

      animator.SetBool("Attack", false);
    }
    public void Shoot(){

      GameObject go = Instantiate(xPrefab,AttackPoint.position, Quaternion.identity);
      Vector3 direction = new Vector3(transform.localScale.x,0);
      go.GetComponent<Projectile>().Setup(direction);
    }

}
