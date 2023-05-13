using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour
{
    [SerializeField] AudioClip heartSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(heartSFX, Camera.main.transform.position);
              collision.GetComponent<CharMovement>().Heal(20);
              Destroy(gameObject);

        }
    }
}
