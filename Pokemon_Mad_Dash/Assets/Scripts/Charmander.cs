using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charmander : MonoBehaviour
{
    [SerializeField] float CharRunSpeed = 5f;

    Rigidbody2D CharRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        CharRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingLeft())
        {
            CharRigidbody.velocity = new Vector2(-CharRunSpeed, 0f);
        }
        else
        {
            CharRigidbody.velocity = new Vector2(CharRunSpeed, 0f);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprites();
    }

    private void FlipSprites()
    {
        transform.localScale = new Vector2(Mathf.Sign(CharRigidbody.velocity.x), 1f);
    }

    private bool isFacingLeft()
    {
        return transform.localScale.x > 0;
    }
}
