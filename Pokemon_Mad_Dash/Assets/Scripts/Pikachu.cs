using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikachu : MonoBehaviour
{
    [SerializeField] GameObject sparkPrefab;

    Animator myAnimator;

    private bool isAttacking = false;
    private float attackTime = 3f;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }
    public void Attack()
    {
        if (!isAttacking)
        {
            myAnimator.SetTrigger("Attack");
            isAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        GameObject spark = Instantiate(sparkPrefab, transform.position, Quaternion.identity);
        spark.transform.parent = transform;
        yield return new WaitForSeconds(attackTime);
        Destroy(spark);
        isAttacking = false;
    }
}
