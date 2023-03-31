using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] float timeToLoad = 2f;
    [SerializeField] AudioClip doorOpenSFX;
    [SerializeField] AudioClip doorClosedSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().SetTrigger("Open Door");
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartLoadingNextLevel();
        }  
    }

    public void StartLoadingNextLevel()
    {
        GetComponent<Animator>().SetTrigger("Close Door");
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(timeToLoad);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
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
