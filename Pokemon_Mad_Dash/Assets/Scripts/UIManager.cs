using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseGameScreen;
    [SerializeField] private GameObject MainMenuScreen;
    [SerializeField] private GameObject SettingScreen;

    AudioSource myMusic;

    private void Awake()
    {
        myMusic = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 4)
        {
            pauseGameScreen.SetActive(false);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SettingScreen.SetActive(false);
            MainMenuScreen.SetActive(true);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    #region Game Over
    public void Restart()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
        pauseGameScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
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


    public void ChangeMusicVolume()
    {
        //get the initial volume of Sound and Change it
        float currentVolume = PlayerPrefs.GetFloat("musicVolume");
        currentVolume += 0.2f;

        //check if the volume reach the maximum and minimum
        if (currentVolume < 0)
        {
            currentVolume = 1;
        }
        else if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        //assign final volume
        myMusic.volume = currentVolume;

        PlayerPrefs.SetFloat("musicVolume", currentVolume);
    }
    #endregion

    public void SettingBackToMenu()
    {
        MainMenuScreen.SetActive(true);
        SettingScreen.SetActive(false);
    }

    public void MenuToSetting()
    {
        SettingScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
    }
}
