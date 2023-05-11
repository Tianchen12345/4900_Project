using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
  [SerializeField] AudioClip diamondSFX;
    //public CharMovement CharMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
          AudioSource.PlayClipAtPoint(diamondSFX, Camera.main.transform.position);
            collision.GetComponent<CharMovement>().Unlock();
            Destroy(gameObject);
        }
    }
}
