using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityStandardAssets.CrossPlatformInput;

public class Tree : MonoBehaviour
{
    PlatformEffector2D myPlatformEffector2D;
    BoxCollider2D myBoxCollider2D;


    void Start()
    {
        myPlatformEffector2D = GetComponent<PlatformEffector2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (CrossPlatformInputManager.GetButton("GoDown"))
            {
                myPlatformEffector2D.rotationalOffset = 180f;
                StartCoroutine(resetPlatform());
            }
        }

    }

    IEnumerator resetPlatform()
    {
        yield return new WaitForSeconds(0.15f);
        myPlatformEffector2D.rotationalOffset = 0f;
    }
}
