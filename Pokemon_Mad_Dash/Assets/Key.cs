using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //[SerializeField] AudioClip heartSFX;
    //public CharMovement CharMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<CharMovement>().Unlock();
            Destroy(gameObject);
        }
    }
}
