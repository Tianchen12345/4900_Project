using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 5, playerDiamond = 10;
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
        scoreText.text = playerDiamond.ToString();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 8)
        {            
            Destroy(gameObject);
        }
    }

    public void addToDiamond(int value)
    {
        playerDiamond += value;
        scoreText.text = playerDiamond.ToString();
    }

    public void addToLives(int value)
    {
        playerLives += value;
        if(playerLives > hearts.Length)
        {
            playerLives = hearts.Length;
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
