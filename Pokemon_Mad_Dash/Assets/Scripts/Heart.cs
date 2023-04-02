using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] AudioClip heartSFX;
    public CharMovement charMovement; // must add charMovement to hearts object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType().ToString().Equals("UnityEngine.BoxCollider2D"))
        {
            AudioSource.PlayClipAtPoint(heartSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().addToLives(1);
            Destroy(gameObject);
        }
        //works with charmander
        if(collision.GetType().ToString().Equals("UnityEngine.CapsuleCollider2D")){
          AudioSource.PlayClipAtPoint(heartSFX, Camera.main.transform.position);
          charMovement.Heal(20);
          Destroy(gameObject);
        }
    }
}
