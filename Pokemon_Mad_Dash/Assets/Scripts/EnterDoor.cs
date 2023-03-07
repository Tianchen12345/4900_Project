using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] AudioClip doorOpenSFX;
    [SerializeField] AudioClip doorClosedSFX;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger("Open Door");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayDoorOpenSFX()
    {
        AudioSource.PlayClipAtPoint(doorOpenSFX, Camera.main.transform.position);
    }

    void PlayDoorClosedSFX()
    {
        AudioSource.PlayClipAtPoint(doorClosedSFX, Camera.main.transform.position);
    }
}
