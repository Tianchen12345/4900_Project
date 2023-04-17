using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] AudioClip heartSFX;
    public CharMovement charMovement; // must add charMovement to hearts object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is PolygonCollider2D)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(heartSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().addToLives(1);
            charMovement.Heal(20);
            Destroy(gameObject);
        }
    }
}
