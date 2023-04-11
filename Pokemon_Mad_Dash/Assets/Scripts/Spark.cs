using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{
    CircleCollider2D myCircleCollider2D;
    private void Awake()
    {
        myCircleCollider2D = GetComponent<CircleCollider2D>();
        Destroy(gameObject, 0.8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && myCircleCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            FindObjectOfType<Player>().BeAttacked();
        }
    }


}
