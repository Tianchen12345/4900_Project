using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    [SerializeField] AudioClip diamondSFX;
    [SerializeField] int diamondValue = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(diamondSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().addToScore(diamondValue);
        Destroy(gameObject);
    }
}
