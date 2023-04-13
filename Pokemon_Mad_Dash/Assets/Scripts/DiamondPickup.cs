using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    [SerializeField] AudioClip diamondSFX;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is PolygonCollider2D)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(diamondSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().addToDiamond(1);
            Destroy(gameObject);
        }

    }
}
