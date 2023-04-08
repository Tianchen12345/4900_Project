using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3, playerScore = 0;
    [SerializeField] Text livesText, scoreText;
    [SerializeField] Image[] hearts;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject GameOverScreen;

    private void Awake()
    {
        Time.timeScale = 1;
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
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 8)
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
            Time.timeScale = 0;
            GameOverScreen.SetActive(true);
            //resetGame();
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

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
