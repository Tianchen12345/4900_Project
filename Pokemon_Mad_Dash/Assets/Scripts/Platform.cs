using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityStandardAssets.CrossPlatformInput;

public class Platform : MonoBehaviour
{
    PlatformEffector2D myPlatformEffector2D;
    TilemapCollider2D myTilemapCollider2D;

    void Start()
    {
        // Get references to the PlatformEffector2D and TilemapCollider2D components.
        myPlatformEffector2D = GetComponent<PlatformEffector2D>();
        myTilemapCollider2D = GetComponent<TilemapCollider2D>();
    }

    void Update()
    {
        // Check if the player is touching the platform.
        if (myTilemapCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            // Check if the "GoDown" button is pressed.
            if (CrossPlatformInputManager.GetButton("GoDown"))
            {
                // Flip the platform and reset it after a short delay.
                myPlatformEffector2D.rotationalOffset = 180f;
                StartCoroutine(resetPlatform());
            }
        }        
    }

    // Coroutine to reset the platform after a short delay.
    IEnumerator resetPlatform()
    {
        yield return new WaitForSeconds(0.15f);
        myPlatformEffector2D.rotationalOffset = 0f; // Reset the platform.
    }
}
