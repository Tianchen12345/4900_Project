using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : MonoBehaviour
{

    [SerializeField] AudioClip diamondSFX;
    public CharCombat charCombat;

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

            if (charCombat != null) // if player is charmander, it will work
            {
                charCombat.HealMana(15);
                //collision.GetComponent<CharCombat>().HealMana(15);
            }
            Destroy(gameObject);
        }
    }
}
