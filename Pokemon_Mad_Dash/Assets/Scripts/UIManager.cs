using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseGameScreen;

    private void Awake()
    {
        pauseGameScreen.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    #region Game Over
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        pauseGameScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Pause Game

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGameScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }

    public void PauseGame(bool status)
    {
        pauseGameScreen.SetActive(status);

        //when pause the game, change timescale to 0 
        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    #endregion
}
