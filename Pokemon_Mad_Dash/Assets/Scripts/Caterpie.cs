using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caterpie : MonoBehaviour
{
    [SerializeField] float CaterpieRunSpeed = 5f;

    Rigidbody2D CaterpieRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        CaterpieRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingLeft())
        {
            CaterpieRigidbody.velocity = new Vector2(-CaterpieRunSpeed, 0f);
        }
        else
        {
            CaterpieRigidbody.velocity = new Vector2(CaterpieRunSpeed, 0f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprites();
    }

    private void FlipSprites()
    {
        transform.localScale = new Vector2(Mathf.Sign(CaterpieRigidbody.velocity.x), 1f);
    }

    private bool isFacingLeft()
    {
        return transform.localScale.x > 0;
    }
}
