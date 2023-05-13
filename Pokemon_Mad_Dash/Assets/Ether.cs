using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ether : MonoBehaviour
{

    [SerializeField] AudioClip diamondSFX;
  

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(diamondSFX, Camera.main.transform.position);
            collision.GetComponent<CharCombat>().HealMana(15);
            Destroy(gameObject);
        }
    }
}
