using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadLine : MonoBehaviour
{
    // Reference to the player object
    public GameObject GameSession;

    BoxCollider2D myBoxCollider2D;

    private void Awake()
    {
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // if palyer is touching the dead line, player will die
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
          //  GameSession.GetComponent<GameSession>().playerLives = 1;
              //GameSession.GetComponent<GameSession>().ProcessPlayerDeath();
        }
    }
}
