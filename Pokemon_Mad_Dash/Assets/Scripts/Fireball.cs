using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 10;
    public float speed;
    private float lifeTime = 5f;
    public float distance;
    public LayerMask whatIsSolid;
    [SerializeField] GameObject impactEffect;

    Rigidbody2D myRigidbody2D;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Invoke("DestroyProjectile", lifeTime);
    }
    private void Update()
    {
        myRigidbody2D.velocity = transform.right * speed;

    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        DestroyProjectile();

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemies>().TakeDamage(damage);
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            DestroyProjectile();
        }
    }

}
