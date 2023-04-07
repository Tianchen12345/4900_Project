using UnityEngine;

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
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            GameSession.GetComponent<GameSession>().playerLives = 1;
            GameSession.GetComponent<GameSession>().ProcessPlayerDeath();
        }
    }
}
