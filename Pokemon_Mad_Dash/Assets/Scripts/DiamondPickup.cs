using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    [SerializeField] AudioClip diamondSFX;
    [SerializeField] int diamondValue = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType().ToString().Equals("UnityEngine.BoxCollider2D"))
        {
            AudioSource.PlayClipAtPoint(diamondSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().addToScore(diamondValue);
            Destroy(gameObject);
        }
        // works with charmander
        if (collision.GetType().ToString().Equals("UnityEngine.CapsuleCollider2D"))
        {
            AudioSource.PlayClipAtPoint(diamondSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().addToScore(diamondValue);
            Destroy(gameObject);
        }
    }
}
