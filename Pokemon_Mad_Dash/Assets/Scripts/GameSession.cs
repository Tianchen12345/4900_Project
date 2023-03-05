using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    private void Awake()
    {
        int numOfGameSession = FindObjectsOfType<GameSession>().Length;

        if(numOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            takeLife();
        }
        else
        {
            resetGame();
        }
    }

    private void takeLife()
    {
        playerLives--;
    }

    private void resetGame()
    {
        SceneManager.LoadScene(3);
        Destroy(gameObject);
    }
}
