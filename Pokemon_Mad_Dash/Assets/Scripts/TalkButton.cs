using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    [SerializeField] GameObject talkButton;
    [SerializeField] GameObject talkUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        talkButton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        talkButton.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(talkButton.activeSelf && Input.GetKeyDown(KeyCode.E)){
            talkUI.SetActive(true);
        }

    }
}
