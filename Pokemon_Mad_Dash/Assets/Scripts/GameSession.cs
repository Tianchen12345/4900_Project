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
    [SerializeField] Image[] hearts;

    [SerializeField] private GameObject Player;

    private void Awake()
    {
        int numOfGameSession = FindObjectsOfType<GameSession>().Length;

        if (numOfGameSession > 1)
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

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            Destroy(gameObject);
        }
    }

    public void addToScore(int value)
    {
        playerScore += value;
        scoreText.text = playerScore.ToString();
    }

    public void addToLives(int value)
    {
        playerLives += value;
        if(playerLives >= 3)
        {
            playerLives = 3;
        }
        updatedHearts();
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
        updatedHearts();
        livesText.text = playerLives.ToString();
    }

    private void updatedHearts()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < playerLives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void resetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
