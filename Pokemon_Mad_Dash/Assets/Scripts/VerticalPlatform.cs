using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    PlatformEffector2D myEffector;
    BoxCollider2D myBoxCollider2D;

    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        myEffector = GetComponent<PlatformEffector2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        VertiPlatform();
    }

    private void VertiPlatform()
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                myEffector.rotationalOffset = 180f;
            }

            StartCoroutine(resetPlatform());
        }
        
    }

    IEnumerator resetPlatform()
    {
        yield return new WaitForSeconds(0.5f);
        myEffector.rotationalOffset = 0f;

    }
}
