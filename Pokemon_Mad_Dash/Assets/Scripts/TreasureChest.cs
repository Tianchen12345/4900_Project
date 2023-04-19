using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] AudioClip treasureChestSFX;
    [SerializeField] GameObject gun;
    Animator myAnimator;

    private bool isOpened = false; // Flag to check if the chest is already opened

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpened && collision.CompareTag("Player"))
        {

            isOpened = true;

            myAnimator.SetTrigger("OpenTreasureChests");
            AudioSource.PlayClipAtPoint(treasureChestSFX, Camera.main.transform.position);
            gun.SetActive(true);

            // Disable the collider so that the chest can't be opened again
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
