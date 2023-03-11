using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] AudioClip heartSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType().ToString().Equals("UnityEngine.BoxCollider2D"))
        {
            AudioSource.PlayClipAtPoint(heartSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().addToLives(1);
            Destroy(gameObject);
        }
    }
}
