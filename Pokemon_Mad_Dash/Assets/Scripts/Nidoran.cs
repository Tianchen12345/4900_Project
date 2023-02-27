using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nidoran : MonoBehaviour
{
    [SerializeField] float NidoranRunSpeed = 5f;

    Rigidbody2D NidoranRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        NidoranRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingLeft())
        {
            NidoranRigidbody.velocity = new Vector2(-NidoranRunSpeed, 0f);
        }
        else
        {
            NidoranRigidbody.velocity = new Vector2(NidoranRunSpeed, 0f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprites();
    }

    private void FlipSprites()
    {
        transform.localScale = new Vector2(Mathf.Sign(NidoranRigidbody.velocity.x), 1f);
    }

    private bool isFacingLeft()
    {
        return transform.localScale.x > 0;
    }
}
