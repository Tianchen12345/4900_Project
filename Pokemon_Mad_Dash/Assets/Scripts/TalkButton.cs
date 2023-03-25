using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    [Header("Talk Button")]
    [SerializeField] private GameObject talkButton;
    [SerializeField] GameObject talkUI;

    [Header("Ink Json")]
    [SerializeField] TextAsset textFile;



    private void Awake()
    {
        talkButton.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            talkButton.SetActive(true);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkButton.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(talkButton.activeSelf && Input.GetKeyDown(KeyCode.E)){
            talkUI.SetActive(true);
        }

    }
}
