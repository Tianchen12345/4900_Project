using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3, playerScore = 0;
    [SerializeField] Text livesText, scoreText;
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

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void addToScore(int value)
    {
        playerScore += value;
        scoreText.text = playerScore.ToString();
    }

    public void addToLives(int value)
    {
        playerLives += value;
        livesText.text = playerLives.ToString();
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
        livesText.text = playerLives.ToString();
    }

    private void resetGame()
    {
        SceneManager.LoadScene(3);
        Destroy(gameObject);
    }
}
