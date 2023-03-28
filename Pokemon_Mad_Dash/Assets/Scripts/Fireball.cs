using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 30;
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyProjectile", lifeTime);
    }
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemies>().TakeDamage(damage);
                DestroyProjectile();
            }

        }
        rb.velocity = transform.right * speed;

    }
    void DestroyProjectile()
    {

        Destroy(gameObject);
    }

}
